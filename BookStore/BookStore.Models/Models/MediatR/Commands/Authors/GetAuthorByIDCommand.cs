using MediatR;

namespace BookStore.Models.Models.MediatR.Commands.Authors
{
    public record GetAuthorByIDCommand(int authorId) : IRequest<Author>
    {
    }
}
