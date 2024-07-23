using Bar_rating.Models;

namespace Bar_rating.Services.Bars
{
    public interface IBarsService
    {
        Task<IEnumerable<Bar>> GetBarsAsync();
        Task<bool> ExistsById(int id);
        Task<Bar> GetBarByIdAsync(int id);
        Task CreateBarAsync(Bar bar);
        Task DeleteBarByIdAsync(int id);
        Task UpdateBar(Bar bar);
    }
}
