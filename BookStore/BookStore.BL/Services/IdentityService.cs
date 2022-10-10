using BookStore.BL.Interfaces;
using BookStore.Models.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace BookStore.BL.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;

        public IdentityService(UserManager<User> userManager, IPasswordHasher<User> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }
        public async Task<User> CheckUserAndPass(string username, string passward)
        {
            var user = await _userManager.FindByNameAsync(username);
            
            if (user == null)
            {
                return null;
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password,passward);

            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<IdentityResult> CreateAsync(User user)
        {
            var exitingUser = await _userManager.GetUserIdAsync(user);
            if (string.IsNullOrEmpty(exitingUser))
            {
                var result = await _userManager.CreateAsync(user);
                return result;
            }
            return IdentityResult.Failed();
        }

        public async Task<IEnumerable<string>> GetUserRoles(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
