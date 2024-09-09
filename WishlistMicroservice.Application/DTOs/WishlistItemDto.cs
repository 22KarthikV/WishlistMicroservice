using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishlistMicroservice.Application.DTOs
{
    public class WishlistItemDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
