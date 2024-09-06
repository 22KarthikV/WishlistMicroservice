using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishlistMicroservice.Application.DTOs
{
    public class AddWishlistItemDto
    {
        public Guid BookId { get; set; }
    }
}
