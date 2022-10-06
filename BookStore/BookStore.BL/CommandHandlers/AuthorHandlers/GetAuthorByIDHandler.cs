using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands.Authors;
using MediatR;

namespace BookStore.BL.CommandHandlers.AuthorHandlers
{
    public class GetAuthorByIDHandler : IRequestHandler<GetAuthorByIDCommand, Author>
    {
        private readonly IAuthorRepo _authorRepo;

        public GetAuthorByIDHandler(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<Author> Handle(GetAuthorByIDCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepo.GetByID(request.authorId);
        }
    }
}
