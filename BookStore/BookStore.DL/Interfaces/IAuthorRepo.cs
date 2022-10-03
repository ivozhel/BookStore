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
        public IEnumerable<Author> GetAllUsers();
        public Author? GetByID(int id);
        public Author? AddUser(Author user);
        public Author? DeleteUser(int id);
        public Author? UpdateUser(Author person);
        public Author? GetAuthorByName(string authorName);

    }
}
