using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepos;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Microsoft.Extensions.Logging;

namespace BookStore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepo _authorRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IAuthorRepo authorRepo, IMapper mapper, ILogger<AuthorService> logger)
        {
            _authorRepo = authorRepo;
            _mapper = mapper;
            _logger = logger;
        }
        public Author? AddUser(AuthorRequest author)
        {

            var auth = _mapper.Map<Author>(author);
            return _authorRepo.AddUser(auth);

        }

        public Author? DeleteUser(int id)
        {
            return _authorRepo?.DeleteUser(id);
        }

        public IEnumerable<Author> GetAllUsers()
        {
            //try
            //{
            //throw new Exception("Test ex");
            //}
            //catch (Exception ex)
            //{

            //_logger.LogError(ex.Message);
            //return null;
            //}
            return _authorRepo.GetAllUsers();

        }

        public Author? GetAuthorByName(string name)
        {
            return _authorRepo.GetAuthorByName(name);
        }

        public Author? GetByID(int id)
        {
            return _authorRepo.GetByID(id);
        }

        public Author? UpdateUser(AuthorRequest author, int id)
        {
            var auth = _mapper.Map<Author>(author);
            auth.ID = id;
            return _authorRepo.UpdateUser(auth);
        }
    }
}
