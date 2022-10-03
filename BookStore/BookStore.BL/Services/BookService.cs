using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {

        private readonly IMapper _mapper;
        private readonly IBookRepo _bookRepo;
        public BookService(IBookRepo bookRepo,IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }
        public Book? AddBook(BookRequest book)
        {
            var bookToAdd = _mapper.Map<Book>(book);
            return _bookRepo.AddBook(bookToAdd);
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

        public bool IsBookDuplicated(BookRequest book)
        {
            if (_bookRepo.GetAllBook().Any(x=>x.Title.Equals(book.Title) && x.AuthorId == book.AuthorId))
            {
                return true;
            }
            return false;
        }

        public Book? UpdateBook(BookRequest book,int id)
        {
            var bookToAdd = _mapper.Map<Book>(book);
            bookToAdd.ID = id;
            return _bookRepo.UpdateBook(bookToAdd);
        }
    }
}
