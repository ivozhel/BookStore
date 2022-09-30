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
        private readonly IBookService _BookRepo;
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger, IBookService bookInMemoryRepo)
        {
            _logger = logger;
            _BookRepo = bookInMemoryRepo;
        }

        [HttpGet(Name = "GetBooks")]
        public IEnumerable<Book> Get()
        {
            return _BookRepo.GetAllBook();
        }
        [HttpGet("ByID")]
        public Book? Get(int id)
        {
            return _BookRepo.GetByID(id);
        }
        [HttpPost]
        public void Add([FromBody] Book book)
        {
            _BookRepo.AddBook(book);
        }
        [HttpPut]
        public Book? Update(Book book)
        {
            return _BookRepo.UpdateBook(book);
        }
        [HttpDelete]
        public Book? Delete(int id)
        {
            return _BookRepo.DeleteBook(id);
        }
    }
}
