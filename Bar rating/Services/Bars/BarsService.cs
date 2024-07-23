using Bar_rating.Models;
using Bar_rating.Services.Reviews;
using Microsoft.EntityFrameworkCore;

namespace Bar_rating.Services.Bars
{
    public class BarsService : IBarsService
    {
        private readonly BarRatingDBContext _context;

        public BarsService(BarRatingDBContext context )
        {
            _context = context;
        }
        
        public async Task CreateBarAsync(Bar bar)
        {
            await _context.Bars.AddAsync(bar);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBarByIdAsync(int id)
        {
            var barToDelete = await GetBarByIdAsync(id);

            List<Review> reviews = _context.Reviews.Where(r => r.BarId == id).ToList();

            foreach (Review review in reviews)
            {
                _context.Remove(review);
            }


            _context.Bars.Remove(barToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(int id) => await _context.Bars.AnyAsync(a => a.Id == id);


        public async Task<Bar> GetBarByIdAsync(int id) => _context.Bars.FirstOrDefault(a => a.Id == id);
         

        public async Task<IEnumerable<Bar>> GetBarsAsync() => await _context.Bars.ToListAsync();

        public async Task UpdateBar(Bar bar)
        {
            _context.Update(bar);
            await _context.SaveChangesAsync();
        }
    }
}
