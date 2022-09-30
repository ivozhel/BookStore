using BookStore.BL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepos;
using BookStore.Models;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet(Name = "GetPersons")]
        public IEnumerable<Person> Get()
        {
            return _personService.GetAllUsers();
        }
        [HttpGet("ByID")]
        public Person? Get(int id)
        {
            return _personService.GetByID(id);
        }
        [HttpPost]
        public void Add([FromBody] Person user)
        {
            _personService.AddUser(user);
        }
        [HttpPut]
        public Person? Update(Person user)
        {
            return _personService.UpdateUser(user);
        }
        [HttpDelete]
        public Person? Delete(int id)
        {
            return _personService.DeleteUser(id);
        }

    }
}