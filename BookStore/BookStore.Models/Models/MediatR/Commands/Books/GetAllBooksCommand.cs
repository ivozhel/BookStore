using MediatR;

namespace BookStore.Models.Models.MediatR.Commands.Books
{
    public record GetAllBooksCommand : IRequest<IEnumerable<Book>>
    {

    }
}
