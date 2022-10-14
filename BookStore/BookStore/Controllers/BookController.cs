using BookStore.Caches.KafkaService;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands.Books;
using BookStore.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly KafkaConsumer<int, Book> _consumer;

        public BookController(IMediator mediator, KafkaConsumer<int, Book> consumer)
        {
            _mediator = mediator;
            _consumer = consumer;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetBooks")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllBooksCommand()));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetBooksFromKafka")]
        public async Task<IActionResult> GetFromKafka()
        {
            return Ok(_consumer.ReturnValues().Count);
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
            var book = await _mediator.Send(new GetBookByIDCommand(id));
            if (book is not null)
            {
                return Ok(book);
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
            if (await _mediator.Send(new IsBookDuplicatedCommand(book)))
            {
                return BadRequest("Book already exists");
            }
            return Ok(await _mediator.Send(new AddBookCommand(book)));
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(BookRequest book, int id)
        {
            if (await _mediator.Send(new GetBookByIDCommand(id)) is null)
            {
                return NotFound("Book with this id dose not exist");
            }
            return Ok(await _mediator.Send(new UpdateBookCommand(book, id)));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _mediator.Send(new GetBookByIDCommand(id)) is null)
            {
                return NotFound("Book with this id dose not exist");
            }
            return Ok(await _mediator.Send(new DeleteBookCommand(id)));
        }
    }
}
