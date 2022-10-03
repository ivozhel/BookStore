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
        public IEnumerable<Book> GetAllBook();
        public Book? GetByID(int id);
        public Book? AddBook(BookRequest book);
        public Book? DeleteBook(int id);
        public Book? UpdateBook(BookRequest book,int id);
        public bool IsBookDuplicated (BookRequest book);
    }
}
