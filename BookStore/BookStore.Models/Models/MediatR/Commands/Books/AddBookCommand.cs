using BookStore.Models.Requests;
using MediatR;

namespace BookStore.Models.Models.MediatR.Commands.Books
{
    public record AddBookCommand(BookRequest book) : IRequest<Book>
    {
    }
}
