using BookStore.BL.Interfaces;
using BookStore.Models.Models.MediatR.Commands.Authors;
using BookStore.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController( IMediator mediator)
        {
            _mediator = mediator;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Name = "GetAuthors")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllAuthorsCommand()));
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
            var author = await _mediator.Send(new GetAuthorByIDCommand(id));
            if (author is not null)
            {
                return Ok(author);
            }
            else
            {
                return NotFound("Author with this id dose not exist");
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] AuthorRequest authorRequest)
        {
            
            var existingAutor = await _mediator.Send(new GetAuthorByNameCommand(authorRequest.Name));
            if (existingAutor is null)
            {
                return Ok(await _mediator.Send(new AddAuthorCommand(authorRequest)));
            }
            else
                return BadRequest("Author already exists");

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(AuthorRequest author, int id)
        {

            if (await _mediator.Send(new GetAuthorByIDCommand(id)) is null)
            {
                return NotFound("Author with this id dose not exist");
            }
            return Ok(await _mediator.Send(new UpdateAuthorCommand(author, id)));
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _mediator.Send(new GetAuthorByIDCommand(id)) is null)
            {
                return NotFound("Author with this id dose not exist");
            }
            ;
            return Ok(await _mediator.Send(new DeleteAuthorCommand(id)));
        }

    }
}