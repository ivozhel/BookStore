using BookStore.Models.Models;
using BookStore.Models.Requests;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        public IEnumerable<Author> GetAllUsers();
        public Author? GetByID(int id);
        public Author? AddUser(AuthorRequest author);
        public Author? DeleteUser(int id);
        public Author? UpdateUser(AuthorRequest author,int id);
        public Author? GetAuthorByName(string name);
    }
}
