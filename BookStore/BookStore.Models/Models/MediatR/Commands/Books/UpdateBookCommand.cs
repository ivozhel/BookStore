using BookStore.Models.Requests;
using MediatR;

namespace BookStore.Models.Models.MediatR.Commands.Books
{
    public record UpdateBookCommand(BookRequest book, int bookId) : IRequest<Book>
    {
    }
}
