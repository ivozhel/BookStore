using BookStore.DL.Interfaces;
using BookStore.Models;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories.InMemoryRepos
{
    public class AuthorInMemoryRepo:IAuthorRepo
    {        
        private static List<Author> _users = new List<Author>() {

            new Author()
            {
                ID = 1,
                Name = "Jon",
                Age = 20,
                Nickname = "Crazy Jon",
                DateOfBirth = DateTime.Now.AddYears(-20)
            },
            new Author()
            {
                ID = 2,
                Name = "Tom",
                Age = 22,
                Nickname = "Tom Tom",
                DateOfBirth = DateTime.Now.AddYears(-22)
            },
            new Author()
            {
                ID = 3,
                Name = "Jon",
                Age = 32,
                Nickname = "Jony boy",
                DateOfBirth = DateTime.Now.AddYears(-32)
            },
        };
        public AuthorInMemoryRepo()
        {

        }

        public IEnumerable<Author> GetAllUsers()
        {
            return _users;
        }

        public Author? GetByID(int id)
        {
            return _users.FirstOrDefault(x => x.ID == id);
        }

        public Author? AddUser(Author user)
        {

            try
            {
                if (GetAuthorByName(user.Name) is null)
                    _users.Add(user);
                else
                    return null;

            }
            catch (Exception ex)
            {
                return null;
            }

            return user;
        }

        public Author? UpdateUser(Author user)
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

        public Author? DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(x => x.ID == id);
            return user;
        }

        public Author? GetAuthorByName(string authorName)
        {
            return _users.FirstOrDefault(x => x.Name.Equals(authorName));
        }
    }
}
