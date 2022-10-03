using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories.InMemoryRepos
{
    public class PersonInMemoryRepo : IPersonRepo
    {
        private static List<Person> _users = new List<Person>() {

            new Person()
            {
                ID = 1,
                Name = "Jon",
                Age = 20,
                DateOfBirth = DateTime.Now.AddYears(-20)
            },
            new Person()
            {
                ID = 2,
                Name = "Tom",
                Age = 22,
                DateOfBirth = DateTime.Now.AddYears(-22)
            },
            new Person()
            {
                ID = 3,
                Name = "Jon",
                Age = 32,
                DateOfBirth = DateTime.Now.AddYears(-32)
            },
        };
        public PersonInMemoryRepo()
        {

        }

        public IEnumerable<Person> GetAllUsers()
        {
            return _users;
        }

        public Person? GetByID(int id)
        {
            return _users.FirstOrDefault(x => x.ID == id);
        }

        public Person? AddUser(Person user)
        {

            try
            {
                _users.Add(user);

            }
            catch (Exception ex)
            {
                return null;
            }

            return user;
        }

        public Person? UpdateUser(Person user)
        {
            var userToUpdate = _users.FirstOrDefault(x => x.ID == user.ID);
            if (userToUpdate == null)
            {
                return null;
            }

            _users.Remove(userToUpdate);
            _users.Add(user);

            return user;
        }

        public Person? DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(x => x.ID == id);
            return user;
        }
    }
}
