using Bar_rating.Models;
using Bar_rating.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace Bar_rating.Services.Reviews
{
    public class ReviewsService : IReviewsService
    {
        private readonly BarRatingDBContext _context;

        public ReviewsService(BarRatingDBContext context)
        {
            _context = context;
        }

        public async Task CreateReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewByIdAsync(int id)
        {
            var reviewToDelete = await GetReviewByIdAsync(id);



            _context.Reviews.Remove(reviewToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(int id) => await _context.Reviews.AnyAsync(a => a.Id == id);


        public async Task<Review> GetReviewByIdAsync(int id) => _context.Reviews.FirstOrDefault(a => a.Id == id);


        public async Task<IEnumerable<Review>> GetReviewsAsync() => await _context.Reviews.ToListAsync();

        public async Task UpdateReviewAsync(Review review)
        {
            _context.Update(review);
            await _context.SaveChangesAsync();
        }
    }
}
