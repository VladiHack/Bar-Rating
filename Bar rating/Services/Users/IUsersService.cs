using Bar_rating.Models;

namespace Bar_rating.Services.Users
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<bool> ExistsById(int id);
        Task<User> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user);
        Task DeleteUserByIdAsync(int id);
    }
}
