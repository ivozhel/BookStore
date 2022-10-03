using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger, IBookService bookInMemoryRepo)
        {
            _logger = logger;
            _bookService = bookInMemoryRepo;
        }

        [HttpGet(Name = "GetBooks")]
        public IEnumerable<Book> Get()
        {
            return _bookService.GetAllBook();
        }
        [HttpGet("ByID")]
        public Book? Get(int id)
        {
            return _bookService.GetByID(id);
        }
        [HttpPost]
        public void Add([FromBody] Book book)
        {
            _bookService.AddBook(book);
        }
        [HttpPut]
        public Book? Update(Book book)
        {
            return _bookService.UpdateBook(book);
        }
        [HttpDelete]
        public Book? Delete(int id)
        {
            return _bookService.DeleteBook(id);
        }
    }
}
