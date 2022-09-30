using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepos;

namespace BookStore.BL.Services
{
    public class BookService : BookInMemoryRepo, IBookService
    {

    }
}
