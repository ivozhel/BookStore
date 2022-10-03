using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepo _bookRepo;
        public BookService(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }
        public Book? AddBook(Book book)
        {
            return _bookRepo.AddBook(book);
        }

        public Book? DeleteBook(int id)
        {
            return _bookRepo.DeleteBook(id);
        }

        public IEnumerable<Book> GetAllBook()
        {
            return _bookRepo.GetAllBook();
        }

        public Book? GetByID(int id)
        {
            return _bookRepo.GetByID(id);
        }

        public Book? UpdateBook(Book book)
        {
            return _bookRepo.UpdateBook(book);
        }
    }
}
