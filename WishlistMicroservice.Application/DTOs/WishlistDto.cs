using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishlistMicroservice.Application.DTOs
{
    public class WishlistDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<WishlistItemDto> Items { get; set; } = new List<WishlistItemDto>();
    }
}
