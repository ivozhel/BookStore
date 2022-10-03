using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Requests
{
    public class BookRequest
    {
        public string Title { get; init; }
        public int AuthorId { get; init; }
    }
}
