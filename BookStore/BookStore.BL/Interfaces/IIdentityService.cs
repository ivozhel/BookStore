using BookStore.Models.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace BookStore.BL.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> CreateAsync(User user);
        Task<User> CheckUserAndPass(string username, string passward);
        Task<IEnumerable<string>> GetUserRoles(User user);
    }
}
