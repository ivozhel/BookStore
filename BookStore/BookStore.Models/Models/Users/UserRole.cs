using Microsoft.AspNetCore.Identity;

namespace BookStore.Models.Models.Users
{
    public class UserRole : IdentityRole
    {
        public int UserId { get; set; }
    }
}
