using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WishlistMicroservice.Application.DTOs;
using WishlistMicroservice.Application.Interfaces;
using WishlistMicroservice.Infrastructure.Services;

namespace WishlistMicroservice.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly IUserServiceClient _userServiceClient;
        private readonly IBookServiceClient _bookServiceClient;

        public WishlistController(IWishlistService wishlistService, IUserServiceClient userServiceClient, IBookServiceClient bookServiceClient)
        {
            _wishlistService = wishlistService;
            _userServiceClient = userServiceClient;
            _bookServiceClient = bookServiceClient;
        }

        [HttpGet]
        public async Task<ActionResult<WishlistDto>> GetWishlist()
        {
            var userInfo = await GetUserInfoAsync();
            var wishlist = await _wishlistService.GetWishlistAsync(userInfo.UserId);
            return Ok(wishlist);
        }

        [HttpPost("items")]
        public async Task<ActionResult<WishlistItemDto>> AddItem([FromBody] AddWishlistItemDto itemDto)
        {
            var userInfo = await GetUserInfoAsync();
            var bookInfo = await _bookServiceClient.GetBookInfoAsync(itemDto.BookId);

            if (bookInfo == null)
            {
                return NotFound("Book not found");
            }

            var addedItem = await _wishlistService.AddItemToWishlistAsync(userInfo.UserId, itemDto);
            return CreatedAtAction(nameof(GetWishlist), addedItem);
        }

        [HttpDelete("items/{bookId}")]
        public async Task<ActionResult> RemoveItem(Guid bookId)
        {
            var userInfo = await GetUserInfoAsync();
            var result = await _wishlistService.RemoveItemFromWishlistAsync(userInfo.UserId, bookId);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetItemCount()
        {
            var userInfo = await GetUserInfoAsync();
            var count = await _wishlistService.GetWishlistItemCountAsync(userInfo.UserId);
            return Ok(count);
        }

        [HttpGet("search")]
        public async Task<ActionResult<WishlistSearchResultDto>> SearchWishlist([FromQuery] string searchTerm, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userInfo = await GetUserInfoAsync();
            var searchResult = await _wishlistService.SearchWishlistAsync(userInfo.UserId, searchTerm, page, pageSize);
            return Ok(searchResult);
        }

        private async Task<UserInfo> GetUserInfoAsync()
        {
            var sessionId = HttpContext.Request.Headers["X-Session-Id"].ToString();
            if (string.IsNullOrEmpty(sessionId))
            {
                throw new UnauthorizedAccessException("Session ID is missing");
            }
            return await _userServiceClient.GetUserInfoAsync(sessionId);
        }
    }
}