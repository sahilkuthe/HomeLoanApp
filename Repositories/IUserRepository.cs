using HomeLoanAPI.Models;

namespace HomeLoanAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> Register(User user);
        Task<User> Login(string email, string password);
        Task<bool> UserExists(string email);

    }
}
