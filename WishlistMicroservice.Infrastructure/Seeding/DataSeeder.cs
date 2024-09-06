using WishlistMicroservice.Domain.Entities;
using WishlistMicroservice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace WishlistMicroservice.Infrastructure.Seeding
{
    public static class DataSeeder
    {
        public static void SeedData(WishlistDbContext context)
        {
            if (!context.Wishlists.Any())
            {
                var wishlist = new Wishlist
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                wishlist.Items.Add(new WishlistItem
                {
                    Id = Guid.NewGuid(),
                    BookId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                    AddedAt = DateTime.UtcNow
                });

                wishlist.Items.Add(new WishlistItem
                {
                    Id = Guid.NewGuid(),
                    BookId = Guid.Parse("a47ac10b-58cc-4372-a567-0e02b2c3d480"),
                    AddedAt = DateTime.UtcNow
                    
                });

                context.Wishlists.Add(wishlist);
                context.SaveChanges();
            }
        }
    }
}