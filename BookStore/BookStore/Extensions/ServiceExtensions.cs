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
            services.AddSingleton<IEmployeeRepo, EmployeeRepository>();
            //services.AddSingleton<IUserRepo, UserStore>();
            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersoneService>();
            services.AddSingleton<IEmplyeeService, EmplyeeService>();
            //services.AddSingleton<IUserService, EmplyeeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            //services.AddSingleton<IAuthorService, AuthorService>();
            //services.AddSingleton<IBookService, BookService>();
            return services;
        }
    }
}
