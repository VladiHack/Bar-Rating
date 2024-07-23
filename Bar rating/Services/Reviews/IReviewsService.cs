using Bar_rating.Models;

namespace Bar_rating.Services.Reviews
{
    public interface IReviewsService
    {
        Task<IEnumerable<Review>> GetReviewsAsync();
        Task<bool> ExistsById(int id);
        Task<Review> GetReviewByIdAsync(int id);
        Task CreateReviewAsync(Review review);
        Task DeleteReviewByIdAsync(int id);
        Task UpdateReviewAsync(Review review);
    }
}
