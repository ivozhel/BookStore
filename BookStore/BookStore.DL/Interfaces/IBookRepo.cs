using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Models;
using BookStore.Models.Requests;

namespace BookStore.DL.Interfaces
{
    public interface IBookRepo
    {
        public Task<IEnumerable<Book>> GetAllBook();
        public Task<Book> GetByID(int id);
        public Task<Book> AddBook(Book book);
        public Task<Book> DeleteBook(int id);
        public Task<Book> UpdateBook(Book book);
        public Task<bool> IsBookDuplicated(BookRequest book);
        public Task<bool> HaveBooks(int id);
    }
}
