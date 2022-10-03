using BookStore.BL.Interfaces;
using BookStore.Models.Requests;
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Name = "GetBooks")]
        public IActionResult Get()
        {
            return Ok(_bookService.GetAllBook());
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("ByID")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var book = _bookService.GetByID(id);
            if (book is not null)
            {
                return Ok(_bookService.GetByID(id));
            }
            else
            {
                return NotFound("Book with this id dose not exist");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult Add([FromBody] BookRequest book)
        {
            if (_bookService.IsBookDuplicated(book))
            {
                BadRequest("Book already exists");
            }
            return Ok(_bookService.AddBook(book));
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(BookRequest book, int id)
        {
            if (_bookService.GetByID(id) is null)
            {
                return NotFound("Book with this id dose not exist");
            }
            return Ok(_bookService.UpdateBook(book, id));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _bookService.GetByID(id);
            return Ok(_bookService.DeleteBook(id));
        }
    }
}
