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
        public async Task<IActionResult> Get()
        {
            return Ok(await _bookService.GetAllBook());
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("ByID")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var book = await _bookService.GetByID(id);
            if (book is not null)
            {
                return Ok(await _bookService.GetByID(id));
            }
            else
            {
                return NotFound("Book with this id dose not exist");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BookRequest book)
        {
            if (await _bookService.IsBookDuplicated(book))
            {
                return BadRequest("Book already exists");
            }
            return Ok(await _bookService.AddBook(book));
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(BookRequest book, int id)
        {
            if (await _bookService.GetByID(id) is null)
            {
                return NotFound("Book with this id dose not exist");
            }
            return Ok(await _bookService.UpdateBook(book, id));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _bookService.GetByID(id) is null)
            {
                return NotFound("Book with this id dose not exist");
            }
            return Ok(await _bookService.DeleteBook(id));
        }
    }
}
