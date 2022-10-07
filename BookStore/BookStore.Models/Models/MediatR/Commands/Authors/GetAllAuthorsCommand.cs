using MediatR;

namespace BookStore.Models.Models.MediatR.Commands.Authors
{
    public record GetAllAuthorsCommand : IRequest<IEnumerable<Author>>
    {
    }
}
