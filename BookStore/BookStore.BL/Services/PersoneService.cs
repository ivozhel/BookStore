using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class PersoneService : IPersonService
    {
        private readonly IPersonRepo _personRepo;
        public PersoneService(IPersonRepo personRepo)
        {
            _personRepo = personRepo;
        }
        // Boris is typing to fast
        public Person? AddUser(Person user)
        {
            return _personRepo.AddUser(user);
        }

        public Person? DeleteUser(int id)
        {
            return _personRepo.DeleteUser(id);
        }

        public IEnumerable<Person> GetAllUsers()
        {
            return _personRepo.GetAllUsers();
        }

        public Person? GetByID(int id)
        {
            return _personRepo.GetByID(id);
        }

        public Person? UpdateUser(Person person)
        {
            return _personRepo.UpdateUser(person);
        }
    }
}
