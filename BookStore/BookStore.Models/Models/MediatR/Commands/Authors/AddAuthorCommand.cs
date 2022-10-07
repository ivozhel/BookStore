using BookStore.Models.Requests;
using MediatR;

namespace BookStore.Models.Models.MediatR.Commands.Authors
{
    public record AddAuthorCommand(AuthorRequest author) : IRequest<Author>
    {
    }
}
