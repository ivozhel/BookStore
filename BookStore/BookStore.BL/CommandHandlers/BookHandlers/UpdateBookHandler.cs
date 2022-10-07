using AutoMapper;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands.Books;
using MediatR;

namespace BookStore.BL.CommandHandlers.BookHandlers
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepo _bookRepo;
        private readonly IAuthorRepo _authorRepo;
        public UpdateBookHandler(IBookRepo bookRepo, IMapper mapper, IAuthorRepo authorRepo)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
            _authorRepo = authorRepo;
        }

        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            if (_authorRepo.GetByID(request.book.AuthorId) is null)
            {
                return null;
            }
            var bookToAdd = _mapper.Map<Book>(request.book);
            bookToAdd.ID = request.bookId;
            return await _bookRepo.UpdateBook(bookToAdd);
        }
    }
}
