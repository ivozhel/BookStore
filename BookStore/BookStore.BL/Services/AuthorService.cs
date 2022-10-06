using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Microsoft.Extensions.Logging;

namespace BookStore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepo _authorRepo;
        private readonly IBookRepo _bookRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IAuthorRepo authorRepo, IMapper mapper, ILogger<AuthorService> logger, IBookRepo bookRepo)
        {
            _authorRepo = authorRepo;
            _mapper = mapper;
            _logger = logger;
            _bookRepo = bookRepo;
        }
        public async Task<Author> AddAuthor(AuthorRequest author)
        {

            var auth = _mapper.Map<Author>(author);
            return await _authorRepo.AddAuthor(auth);

        }

        public async Task<Author> DeleteAuthor(int id)
        {
            if (await _bookRepo.HaveBooks(id))
            {
                return null;
            }
            return await _authorRepo.DeleteAuthor(id);
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _authorRepo.GetAll();
        }

        public async Task<Author> GetAuthorByName(string name)
        {
            return await _authorRepo.GetAuthorByName(name);
        }

        public async Task<Author> GetByID(int id)
        {
            return await _authorRepo.GetByID(id);
        }

        public async Task<Author> UpdateAuthor(AuthorRequest author, int id)
        {
            var auth = _mapper.Map<Author>(author);
            auth.ID = id;
            return await _authorRepo.UpdateAuthor(auth);
        }
    }
}
