using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands;
using BookStore.Models.Models.MediatR.Commands.Books;
using MediatR;

namespace BookStore.BL.CommandHandlers.BookHandlers
{
    public class GetBookByIDHandler : IRequestHandler<GetBookByIDCommand, Book>
    {
        private readonly IBookRepo _bookRepo;

        public GetBookByIDHandler(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<Book> Handle(GetBookByIDCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepo.GetByID(request.bookId);
        }
    }
}
