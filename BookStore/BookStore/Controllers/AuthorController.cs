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
        public IActionResult Get()
        {
            return Ok(_authorService.GetAllUsers());
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
            var author = _authorService.GetByID(id);
            if (author is not null)
            {
                return Ok(_authorService.GetByID(id));
            }
            else
            {
                return NotFound("Author with this id dose not exist");
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] AuthorRequest authorRequest)
        {
            var existingAutor = _authorService.GetAuthorByName(authorRequest.Name);
            if (existingAutor is null)
            {
                return Ok(_authorService.AddUser(authorRequest));
            }
            else
                return BadRequest("Author already exists");

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(AuthorRequest author, int id)
        {
            if (_authorService.GetByID(id) is null)
            {
                return NotFound("Author with this id dose not exist");
            }

            return Ok(_authorService.UpdateUser(author, id));
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            if (_authorService.GetByID(id) is null)
            {
                return NotFound("Author with this id dose not exist");
            }

            return Ok(_authorService.DeleteUser(id));
        }

    }
}