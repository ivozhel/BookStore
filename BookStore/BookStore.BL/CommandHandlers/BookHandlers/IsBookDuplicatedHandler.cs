using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.DL.Interfaces;
using BookStore.Models.Models.MediatR.Commands.Books;
using MediatR;

namespace BookStore.BL.CommandHandlers.BookHandlers
{
    public class IsBookDuplicatedHandler : IRequestHandler<IsBookDuplicatedCommand, bool>
    {
        private readonly IBookRepo _bookRepo;

        public IsBookDuplicatedHandler(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<bool> Handle(IsBookDuplicatedCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepo.IsBookDuplicated(request.book);
        }
    }
}
