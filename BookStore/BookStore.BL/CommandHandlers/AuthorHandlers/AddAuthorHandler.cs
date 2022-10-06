using AutoMapper;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands.Authors;
using MediatR;

namespace BookStore.BL.CommandHandlers.AuthorHandlers
{
    public class AddAuthorHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepo _authorRepo;
        public AddAuthorHandler(IMapper mapper, IAuthorRepo authorRepo)
        {
            _mapper = mapper;
            _authorRepo = authorRepo;
        }

        public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            var auth = _mapper.Map<Author>(request.author);
            return await _authorRepo.AddAuthor(auth);
        }
    }
}
