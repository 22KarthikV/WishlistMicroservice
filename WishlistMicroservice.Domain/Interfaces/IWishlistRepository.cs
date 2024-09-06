using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WishlistMicroservice.Domain.Entities;

namespace WishlistMicroservice.Domain.Interfaces
{
    public interface IWishlistRepository
    {
        Task<Wishlist> GetByIdAsync(Guid id);
        Task<Wishlist> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<WishlistItem>> GetWishlistItemsAsync(Guid wishlistId);
        Task<WishlistItem> AddItemAsync(WishlistItem item);
        Task<bool> RemoveItemAsync(Guid wishlistId, Guid bookId);
        Task<int> GetItemCountAsync(Guid wishlistId);
        Task<IEnumerable<WishlistItem>> SearchItemsAsync(Guid wishlistId, string searchTerm, int page, int pageSize);
    }
}