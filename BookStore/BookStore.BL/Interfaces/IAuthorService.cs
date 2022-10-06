using BookStore.Models.Models;
using BookStore.Models.Requests;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        public Task<IEnumerable<Author>> GetAll();
        public Task<Author> GetByID(int id);
        public Task<Author> AddAuthor(AuthorRequest author);
        public Task<Author> DeleteAuthor(int id);
        public Task<Author> UpdateAuthor(AuthorRequest author,int id);
        public Task<Author> GetAuthorByName(string name);
    }
}
