using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishlistMicroservice.Application.DTOs
{
    public class WishlistDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<WishlistItemDto> Items { get; set; } = new List<WishlistItemDto>();
    }
}
