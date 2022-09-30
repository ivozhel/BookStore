using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories.InMemoryRepos
{
    public class BookInMemoryRepo : IBookRepo
    {
        private static List<Book> _books = new List<Book>() {

            new Book()
            {
                ID = 1,
                Title = "Book1",
                AuthorId = 1,
            },
            new Book()
            {
                ID = 2,
                Title = "Book2",
                AuthorId = 2,
            },
            new Book()
            {    
                ID = 3,
                Title = "Book3",
                AuthorId = 3,
            },
        };
        public BookInMemoryRepo()
        {

        }

        public IEnumerable<Book> GetAllBook()
        {
            return _books;
        }

        public Book? GetByID(int id)
        {
            return _books.FirstOrDefault(x => x.ID == id);
        }

        public Book? AddBook(Book book)
        {

            try
            {
                _books.Add(book);

            }
            catch (Exception ex)
            {
                return null;
            }

            return book;
        }

        public Book? UpdateBook(Book book)
        {
            var bookToUpdate = _books.FirstOrDefault(x => x.ID == book.ID);
            if (bookToUpdate == null)
            {
                return null;
            }

            _books.Remove(bookToUpdate);
            _books.Add(book);

            return book;
        }

        public Book? DeleteBook(int id)
        {
            var book = _books.FirstOrDefault(x => x.ID == id);
            return book;
        }

    }
}
