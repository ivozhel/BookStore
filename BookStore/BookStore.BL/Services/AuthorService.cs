using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepos;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepo _authorRepo;

        public AuthorService(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }
        public Author? AddUser(Author author)
        {
            return _authorRepo.AddUser(author);
        }

        public Author? DeleteUser(int id)
        {
            return _authorRepo?.DeleteUser(id);
        }

        public IEnumerable<Author> GetAllUsers()
        {
            return _authorRepo.GetAllUsers();
        }

        public Author? GetByID(int id)
        {
            return _authorRepo.GetByID(id);
        }

        public Author? UpdateUser(Author author)
        {
            return _authorRepo.UpdateUser(author);
        }
    }
}
