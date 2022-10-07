using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepos;
using BookStore.DL.Repositories.MsSQL;

namespace BookStore.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterRepos(this IServiceCollection services)
        {
            services.AddSingleton<IPersonRepo, PersonInMemoryRepo>();
            services.AddSingleton<IAuthorRepo, AuthorRepository>();
            services.AddSingleton<IBookRepo, BookRepository>();
            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersoneService>();
            //services.AddSingleton<IAuthorService, AuthorService>();
            //services.AddSingleton<IBookService, BookService>();
            return services;
        }
    }
}
