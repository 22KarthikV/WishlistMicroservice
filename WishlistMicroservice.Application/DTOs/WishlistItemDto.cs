using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishlistMicroservice.Application.DTOs
{
    public class WishlistItemDto
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
