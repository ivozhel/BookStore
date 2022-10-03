using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepos;
using BookStore.Models.Models;
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

        [HttpGet(Name = "GetAuthors")]
        public IEnumerable<Author> Get()
        {
            return _authorService.GetAllUsers();
        }
        [HttpGet("ByID")]
        public Author? Get(int id)
        {
            return _authorService.GetByID(id);
        }
        [HttpPost]
        public void Add([FromBody] Author user)
        {
            _authorService.AddUser(user);
        }
        [HttpPut]
        public Author? Update(Author user)
        {
            return _authorService.UpdateUser(user);
        }
        [HttpDelete]
        public Author? Delete(int id)
        {
            return _authorService.DeleteUser(id);
        }

    }
}