using AutoMapper;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands.Books;
using MediatR;

namespace BookStore.BL.CommandHandlers.BookHandlers
{
    internal class AddBookHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepo _bookRepo;
        private readonly IAuthorRepo _authorRepo;

        public AddBookHandler(IMapper mapper, IBookRepo bookRepo, IAuthorRepo authorRepo)
        {
            _mapper = mapper;
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
        }



        public async Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (await _authorRepo.GetByID(request.book.AuthorId) is null)
            {
                return null;
            }
            var bookToAdd = _mapper.Map<Book>(request.book);
            return await _bookRepo.AddBook(bookToAdd);
        }
    }
}
