using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WishlistMicroservice.Domain.Entities;

namespace WishlistMicroservice.Domain.Interfaces
{
    public interface IWishlistRepository
    {
        Task<Wishlist> GetByIdAsync(int id);
        Task<Wishlist> GetByUserIdAsync(string userId);
        Task<IEnumerable<WishlistItem>> GetWishlistItemsAsync(int wishlistId);
        Task<WishlistItem> AddItemAsync(WishlistItem item);
        Task<bool> RemoveItemAsync(int wishlistId, int bookId);
        Task<int> GetItemCountAsync(int wishlistId);
        Task<IEnumerable<WishlistItem>> SearchItemsAsync(int wishlistId, string searchTerm, int page, int pageSize);
    }
}