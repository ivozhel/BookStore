using BookStore.Models.Requests;
using MediatR;

namespace BookStore.Models.Models.MediatR.Commands.Books
{
    public record IsBookDuplicatedCommand(BookRequest book) : IRequest<bool>
    {

    }
}
