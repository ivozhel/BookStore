using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Requests;
using MediatR;

namespace BookStore.Models.Models.MediatR.Commands.Authors
{
    public record UpdateAuthorCommand(AuthorRequest author,int authorId) : IRequest<Author>
    {
    }
}
