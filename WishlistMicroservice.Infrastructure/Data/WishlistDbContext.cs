using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using WishlistMicroservice.Domain.Entities;

namespace WishlistMicroservice.Infrastructure.Data
{
    public class WishlistDbContext : DbContext
    {
        public WishlistDbContext(DbContextOptions<WishlistDbContext> options) : base(options) { }

        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wishlist>()
                .HasMany(w => w.Items)
                .WithOne()
                .HasForeignKey(wi => wi.WishlistId);

            modelBuilder.Entity<WishlistItem>()
                .HasIndex(wi => new { wi.WishlistId, wi.BookId })
                .IsUnique();
        }
    }
}