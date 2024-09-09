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
                    
                    UserId = "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                wishlist.Items.Add(new WishlistItem
                {
                 
                    BookId = 1,
                    AddedAt = DateTime.UtcNow
                });

                wishlist.Items.Add(new WishlistItem
                {
                 
                    BookId = 2,
                    AddedAt = DateTime.UtcNow
                    
                });

                context.Wishlists.Add(wishlist);
                context.SaveChanges();
            }
        }
    }
}