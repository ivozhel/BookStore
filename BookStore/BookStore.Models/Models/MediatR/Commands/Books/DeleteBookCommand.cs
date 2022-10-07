using MediatR;

namespace BookStore.Models.Models.MediatR.Commands.Books
{
    public record DeleteBookCommand(int bookId) : IRequest<Book>
    {
    }
}
