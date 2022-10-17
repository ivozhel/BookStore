using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models.Users
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
