using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    internal interface IBookRepo
    {
        public IEnumerable<Book> GetAllBook();
        public Book? GetByID(int id);
        public Book? AddBook(Book user);
        public Book? DeleteBook(int id);
        public Book? UpdateBook(Book person);
    }
}
