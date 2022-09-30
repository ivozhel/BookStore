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
        private readonly IAuthorRepo _authorRepo;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(ILogger<AuthorController> logger, IAuthorRepo userInMemoryRepo)
        {
            _logger = logger;
            _authorRepo = userInMemoryRepo;
        }

        [HttpGet(Name = "GetAuthors")]
        public IEnumerable<Author> Get()
        {
            return _authorRepo.GetAllUsers();
        }
        [HttpGet("ByID")]
        public Author? Get(int id)
        {
            return _authorRepo.GetByID(id);
        }
        [HttpPost]
        public void Add([FromBody] Author user)
        {
            _authorRepo.AddUser(user);
        }
        [HttpPut]
        public Author? Update(Author user)
        {
            return _authorRepo.UpdateUser(user);
        }
        [HttpDelete]
        public Author? Delete(int id)
        {
            return _authorRepo.DeleteUser(id);
        }

    }
}