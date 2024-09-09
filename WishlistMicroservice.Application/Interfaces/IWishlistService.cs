using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WishlistMicroservice.Application.DTOs;

namespace WishlistMicroservice.Application.Interfaces
{
    public interface IWishlistService
    {
        Task<WishlistDto> GetWishlistAsync(string userId);
        Task<WishlistItemDto> AddItemToWishlistAsync(string userId, AddWishlistItemDto itemDto);
        Task<bool> RemoveItemFromWishlistAsync(string userId, int bookId);
        Task<int> GetWishlistItemCountAsync(string userId);
        Task<WishlistSearchResultDto> SearchWishlistAsync(string userId, string searchTerm, int page, int pageSize);
    }
}
