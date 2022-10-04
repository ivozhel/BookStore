using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Models;
using BookStore.Models.Requests;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<Book>> GetAllBook();
        public Task<Book> GetByID(int id);
        public Task<Book> AddBook(BookRequest book);
        public Task<Book> DeleteBook(int id);
        public Task<Book> UpdateBook(BookRequest book,int id);
        public Task<bool> IsBookDuplicated (BookRequest book);
    }
}
