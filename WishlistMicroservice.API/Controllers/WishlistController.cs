using System;
using System.Security.Claims;
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

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet]
        public async Task<ActionResult<WishlistDto>> GetWishlist()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var wishlist = await _wishlistService.GetWishlistAsync(userId);
            return Ok(wishlist);
        }

        [HttpPost("items")]
        public async Task<ActionResult<WishlistItemDto>> AddItem([FromBody] AddWishlistItemDto itemDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            var addedItem = await _wishlistService.AddItemToWishlistAsync(userId, itemDto);
            return CreatedAtAction(nameof(GetWishlist), addedItem);
        }

        [HttpDelete("items/{bookId}")]
        public async Task<ActionResult> RemoveItem(int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _wishlistService.RemoveItemFromWishlistAsync(userId, bookId);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetItemCount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var count = await _wishlistService.GetWishlistItemCountAsync(userId);
            return Ok(count);
        }

        [HttpGet("search")]
        public async Task<ActionResult<WishlistSearchResultDto>> SearchWishlist([FromQuery] string searchTerm, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var searchResult = await _wishlistService.SearchWishlistAsync(userId, searchTerm, page, pageSize);
            return Ok(searchResult);
        }
    }
}