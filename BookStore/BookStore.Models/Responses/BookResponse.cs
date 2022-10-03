using BookStore.Models.Models;

namespace BookStore.Models.Responses
{
    public class BookResponse : BaseResponse
    {
        public Book Book { get; set; }
    }
}
