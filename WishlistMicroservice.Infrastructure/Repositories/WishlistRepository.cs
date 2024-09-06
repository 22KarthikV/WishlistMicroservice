using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WishlistMicroservice.Domain.Entities;
using WishlistMicroservice.Domain.Interfaces;
using WishlistMicroservice.Infrastructure.Data;

namespace WishlistMicroservice.Infrastructure.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly WishlistDbContext _context;

        public WishlistRepository(WishlistDbContext context)
        {
            _context = context;
        }

        public async Task<Wishlist> GetByIdAsync(Guid id)
        {
            return await _context.Wishlists
                .Include(w => w.Items)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Wishlist> GetByUserIdAsync(Guid userId)
        {
            return await _context.Wishlists
                .Include(w => w.Items)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task<IEnumerable<WishlistItem>> GetWishlistItemsAsync(Guid wishlistId)
        {
            return await _context.WishlistItems
                .Where(wi => wi.WishlistId == wishlistId)
                .ToListAsync();
        }
        
        public async Task<WishlistItem> AddItemAsync(WishlistItem item)
        {
            await _context.WishlistItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> RemoveItemAsync(Guid wishlistId, Guid bookId)
        {
            var item = await _context.WishlistItems
                .FirstOrDefaultAsync(wi => wi.WishlistId == wishlistId && wi.BookId == bookId);

            if (item == null)
                return false;

            _context.WishlistItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetItemCountAsync(Guid wishlistId)
        {
            return await _context.WishlistItems
                .CountAsync(wi => wi.WishlistId == wishlistId);
        }

        public async Task<IEnumerable<WishlistItem>> SearchItemsAsync(Guid wishlistId, string searchTerm, int page, int pageSize)
        {
            return await _context.WishlistItems
                .Where(wi => wi.WishlistId == wishlistId && wi.BookId.ToString().Contains(searchTerm))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        
    }
}