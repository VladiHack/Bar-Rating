using Bar_rating.Models;
using Bar_rating.Services.Bars;
using Microsoft.EntityFrameworkCore;

namespace Bar_rating.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly BarRatingDBContext _context;

        public UsersService(BarRatingDBContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            var userToDelete = await GetUserByIdAsync(id);

            List<Review> reviews = _context.Reviews.Where(r => r.UserId == id).ToList();
            foreach (Review review in reviews)
            {
                _context.Reviews.Remove(review);
            }

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(int id) => await _context.Users.AnyAsync(a => a.Id == id);


        public async Task<User> GetUserByIdAsync(int id) => _context.Users.FirstOrDefault(a => a.Id == id);


        public async Task<IEnumerable<User>> GetUsersAsync() => await _context.Users.ToListAsync();

        public async Task UpdateUser(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
