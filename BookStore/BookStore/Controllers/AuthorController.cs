using BookStore.BL.Interfaces;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Name = "GetAuthors")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _authorService.GetAll());
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
            var author = await _authorService.GetByID(id);
            if (author is not null)
            {
                return Ok(await _authorService.GetByID(id));
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
            var existingAutor = await _authorService.GetAuthorByName(authorRequest.Name);
            if (existingAutor is null)
            {
                return Ok(await _authorService.AddAuthor(authorRequest));
            }
            else
                return BadRequest("Author already exists");

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(AuthorRequest author, int id)
        {
            if (await _authorService.GetByID(id) is null)
            {
                return NotFound("Author with this id dose not exist");
            }

            return Ok(await _authorService.UpdateAuthor(author, id));
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _authorService.GetByID(id) is null)
            {
                return NotFound("Author with this id dose not exist");
            }

            return Ok(await _authorService.DeleteAuthor(id));
        }

    }
}