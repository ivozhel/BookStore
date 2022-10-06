using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands.Authors;
using MediatR;

namespace BookStore.BL.CommandHandlers.AuthorHandlers
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, Author>
    {
        private readonly IAuthorRepo _authorRepo;
        private readonly IMapper _mapper;
        public UpdateAuthorHandler(IAuthorRepo authorRepo, IMapper mapper)
        {
            _authorRepo = authorRepo;
            _mapper = mapper;
        }
        public async Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var auth = _mapper.Map<Author>(request.author);
            auth.ID = request.authorId;
            return await _authorRepo.UpdateAuthor(auth);
        }
    }
}
