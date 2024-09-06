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
        Task<WishlistDto> GetWishlistAsync(Guid userId);
        Task<WishlistItemDto> AddItemToWishlistAsync(Guid userId, AddWishlistItemDto itemDto);
        Task<bool> RemoveItemFromWishlistAsync(Guid userId, Guid bookId);
        Task<int> GetWishlistItemCountAsync(Guid userId);
        Task<WishlistSearchResultDto> SearchWishlistAsync(Guid userId, string searchTerm, int page, int pageSize);
    }
}
