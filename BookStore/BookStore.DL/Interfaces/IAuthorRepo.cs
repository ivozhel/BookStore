using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IAuthorRepo
    {
        public Task<IEnumerable<Author>> GetAll();
        public Task<Author> GetByID(int id);
        public Task<Author> AddAuthor(Author user);
        public Task<Author> DeleteAuthor(int id);
        public Task<Author> UpdateAuthor(Author person);
        public Task<Author> GetAuthorByName(string authorName);

    }
}
