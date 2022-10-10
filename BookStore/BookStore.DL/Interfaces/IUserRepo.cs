using BookStore.Models.Models.Users;

namespace BookStore.DL.Interfaces
{
    public interface IUserRepo
    {
        public Task<User> GetUsersInfo(string email,string password);
    }
}
