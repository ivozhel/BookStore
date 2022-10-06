using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands.Books;
using MediatR;

namespace BookStore.BL.CommandHandlers.BookHandlers
{
    internal class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly IBookRepo _bookRepo;

        public DeleteBookHandler(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<Book> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepo.DeleteBook(request.bookId);
        }
    }
}
