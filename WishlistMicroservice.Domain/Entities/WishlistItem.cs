using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishlistMicroservice.Domain.Entities
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public int WishlistId { get; set; }
        public int BookId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
