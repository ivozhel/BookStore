using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands.Authors;
using MediatR;

namespace BookStore.BL.CommandHandlers.AuthorHandlers
{
    public class GetAuthorByNameHandler : IRequestHandler<GetAuthorByNameCommand, Author>
    {
        private readonly IAuthorRepo _authorRepo;

        public GetAuthorByNameHandler(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<Author> Handle(GetAuthorByNameCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepo.GetAuthorByName(request.name);
        }
    }
}
