using MediatR;

namespace BookStore.Models.Models.MediatR.Commands.Authors
{
    public record DeleteAuthorCommand(int authorId) : IRequest<Author>
    {
    }
}
