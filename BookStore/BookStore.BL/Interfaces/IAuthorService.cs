using BookStore.Models.Models;
using BookStore.Models.Requests;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        public Task<IEnumerable<Author>> GetAll();
        public Task<Author> GetByID(int id);
        public Task<Author> AddUser(AuthorRequest author);
        public Task<Author> DeleteUser(int id);
        public Task<Author> UpdateUser(AuthorRequest author,int id);
        public Task<Author> GetAuthorByName(string name);
    }
}
