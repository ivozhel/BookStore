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
        private readonly IAuthorRepo _authorRepo;
        public BookService(IBookRepo bookRepo,IMapper mapper, IAuthorRepo authorRepo)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
            _authorRepo = authorRepo;
        }
        public async Task<Book> AddBook(BookRequest book)
        {
            if (await _authorRepo.GetByID(book.AuthorId) is null)
            {
                return null;
            }
            var bookToAdd = _mapper.Map<Book>(book);
            return await _bookRepo.AddBook(bookToAdd);
        }

        public async Task<Book> DeleteBook(int id)
        {
            return await _bookRepo.DeleteBook(id);
        }

        public async Task<IEnumerable<Book>> GetAllBook()
        {
            return await _bookRepo.GetAllBook();
        }

        public async Task<Book> GetByID(int id)
        {
            return await _bookRepo.GetByID(id);
        }

        public async Task<bool> IsBookDuplicated(BookRequest book)
        {
            return await _bookRepo.IsBookDuplicated(book);
        }

        public async Task<Book> UpdateBook(BookRequest book,int id)
        {
            if(_authorRepo.GetByID(book.AuthorId) is null)
            {
                return null;
            }
            var bookToAdd = _mapper.Map<Book>(book);
            bookToAdd.ID = id;
            return await _bookRepo.UpdateBook(bookToAdd);
        }
    }
}
