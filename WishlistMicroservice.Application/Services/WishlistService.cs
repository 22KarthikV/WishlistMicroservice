using System;
using System.Linq;
using System.Threading.Tasks;
using WishlistMicroservice.Application.DTOs;
using WishlistMicroservice.Application.Interfaces;
using WishlistMicroservice.Domain.Entities;
using WishlistMicroservice.Domain.Interfaces;

namespace WishlistMicroservice.Application.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public async Task<WishlistDto> GetWishlistAsync(Guid userId)
        {
            var wishlist = await _wishlistRepository.GetByUserIdAsync(userId);
            return MapToDto(wishlist);
        }

        public async Task<WishlistItemDto> AddItemToWishlistAsync(Guid userId, AddWishlistItemDto itemDto)
        {
            var wishlist = await _wishlistRepository.GetByUserIdAsync(userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist { Id = Guid.NewGuid(), UserId = userId, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            }

            var newItem = new WishlistItem
            {
                Id = Guid.NewGuid(),
                WishlistId = wishlist.Id,
                BookId = itemDto.BookId,
                AddedAt = DateTime.UtcNow
            };

            var addedItem = await _wishlistRepository.AddItemAsync(newItem);
            return MapToDto(addedItem);
        }

        public async Task<bool> RemoveItemFromWishlistAsync(Guid userId, Guid bookId)
        {
            var wishlist = await _wishlistRepository.GetByUserIdAsync(userId);
            if (wishlist == null)
            {
                return false;
            }

            return await _wishlistRepository.RemoveItemAsync(wishlist.Id, bookId);
        }

        public async Task<int> GetWishlistItemCountAsync(Guid userId)
        {
            var wishlist = await _wishlistRepository.GetByUserIdAsync(userId);
            if (wishlist == null)
            {
                return 0;
            }

            return await _wishlistRepository.GetItemCountAsync(wishlist.Id);
        }

        public async Task<WishlistSearchResultDto> SearchWishlistAsync(Guid userId, string searchTerm, int page, int pageSize)
        {
            var wishlist = await _wishlistRepository.GetByUserIdAsync(userId);
            if (wishlist == null)
            {
                return new WishlistSearchResultDto { Items = Enumerable.Empty<WishlistItemDto>(), TotalCount = 0, Page = page, PageSize = pageSize };
            }

            var items = await _wishlistRepository.SearchItemsAsync(wishlist.Id, searchTerm, page, pageSize);
            var totalCount = await _wishlistRepository.GetItemCountAsync(wishlist.Id);

            return new WishlistSearchResultDto
            {
                Items = items.Select(MapToDto),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        private WishlistDto MapToDto(Wishlist wishlist)
        {
            return new WishlistDto
            {
                Id = wishlist.Id,
                UserId = wishlist.UserId,
                CreatedAt = wishlist.CreatedAt,
                UpdatedAt = wishlist.UpdatedAt,
                Items = wishlist.Items.Select(MapToDto).ToList()
            };
        }

        private WishlistItemDto MapToDto(WishlistItem item)
        {
            return new WishlistItemDto
            {
                Id = item.Id,
                BookId = item.BookId,
                AddedAt = item.AddedAt
            };
        }
    }
}