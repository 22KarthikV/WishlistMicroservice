using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishlistMicroservice.Domain.Entities
{
    public class WishlistItem
    {
        public Guid Id { get; set; }
        public Guid WishlistId { get; set; }
        public Guid BookId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
