using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands.Authors;
using MediatR;

namespace BookStore.BL.CommandHandlers.AuthorHandlers
{
    public class AuthorDeleteHandler : IRequestHandler<DeleteAuthorCommand, Author>
    {
        private readonly IAuthorRepo _authorRepo;
        private readonly IBookRepo _bookRepo;

        public AuthorDeleteHandler(IBookRepo bookRepo, IAuthorRepo authorRepo)
        {
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
        }

        public async Task<Author> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            if (await _bookRepo.HaveBooks(request.authorId))
            {
                return null;
            }
            return await _authorRepo.DeleteAuthor(request.authorId);
        }
    }
}
