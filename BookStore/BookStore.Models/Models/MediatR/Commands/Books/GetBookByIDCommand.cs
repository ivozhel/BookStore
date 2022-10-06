using MediatR;

namespace BookStore.Models.Models.MediatR.Commands.Books
{
    public record GetBookByIDCommand(int bookId) : IRequest<Book>
    {
    }
}
