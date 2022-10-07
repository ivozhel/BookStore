using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookStore.Models.Models.MediatR.Commands.Authors
{
    public record GetAuthorByNameCommand(string name) : IRequest<Author>
    {
    }
}
