using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WishlistMicroservice.Application.Interfaces;
using WishlistMicroservice.Application.Services;
using WishlistMicroservice.Domain.Interfaces;
using WishlistMicroservice.Infrastructure.Data;
using WishlistMicroservice.Infrastructure.Repositories;
using WishlistMicroservice.Infrastructure.Seeding;
using WishlistMicroservice.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext
builder.Services.AddDbContext<WishlistDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register dependencies
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddHttpClient<IUserServiceClient, UserServiceClient>();
builder.Services.AddHttpClient<IBookServiceClient, BookServiceClient>();

// Add memory cache
builder.Services.AddMemoryCache();

// Configure JWT authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["UserService:Authority"];
        options.Audience = builder.Configuration["UserService:Audience"];
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Seed data
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<WishlistDbContext>();
        context.Database.Migrate(); // Ensure the database is created and up to date
        DataSeeder.SeedData(context);
    }
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();