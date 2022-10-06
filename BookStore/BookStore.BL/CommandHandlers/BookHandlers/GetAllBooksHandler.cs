using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands.Books;
using MediatR;

namespace BookStore.BL.CommandHandlers.BookHandlers
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksCommand, IEnumerable<Book>>
    {
        private readonly IBookRepo _bookRepo;

        public GetAllBooksHandler(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<IEnumerable<Book>> Handle(GetAllBooksCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepo.GetAllBook();
        }
    }
}
