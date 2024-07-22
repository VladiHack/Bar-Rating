

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Resources;
using Bar_rating.Models;
using Bar_rating.Validator;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bar_rating.Controllers
{
    public class BarController:Controller
    {
        private readonly BarRatingDBContext _context;

        public BarController(BarRatingDBContext context)
        {
            _context=context;
        }

        public IActionResult Index()
        {
      

            ViewBag.userId = UserIdSupplier.id;
            ViewBag.role = RoleSupplier.role;
            return View(_context.Bars.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bar bar)
        {
            string msg = "";
            List<Bar> bars = _context.Bars.ToList();
            if (bars.Count == 0)
            {
                bar.Id = 1;
            }
            else
            {
                bar.Id = bars[bars.Count() - 1].Id + 1;
            }
            if (bar.BarImageFile != null && bar.BarImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await bar.BarImageFile.CopyToAsync(memoryStream);
                    bar.BarImage = memoryStream.ToArray();
                }
            }
            
            msg = BarValidator.ReturnErrorsCreate(bars, bar);
            ViewBag.Message = msg;

            if (msg == "")
            {
                _context.Bars.Add(bar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bar);
        }
     

        public IActionResult Edit(int id)
        {
            ViewBag.userId = UserIdSupplier.id;
            var users = _context.Bars.AsNoTracking().FirstOrDefault(y => y.Id == id);
            return View(users);
        }
        public IActionResult Delete(int id)
        {
            ViewBag.userId = UserIdSupplier.id;
            var users = _context.Bars.FirstOrDefault(x => x.Id == id);
            return View(users);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Изтриваме всички ревюта за този бар
            var bar = await _context.Bars.FindAsync(id);
            if (bar != null)
            {
                foreach (var review in _context.Reviews.ToList())
                {
                    if (bar.Id == review.BarId)
                    {
                        _context.Remove(review);
                    }
                }
                _context.Bars.Remove(bar);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Bar bar)
        {
            Bar oldBar = _context.Bars.AsNoTracking().FirstOrDefault(p => p.Id == bar.Id);
            List<Bar> bars = _context.Bars.AsNoTracking().ToList();

            string msg = "";
            msg = BarValidator.ReturnErrorsEdit(bars, oldBar, oldBar.Id);
            ViewBag.Message = msg;
            if (bar.BarImageFile != null && bar.BarImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await bar.BarImageFile.CopyToAsync(memoryStream);
                    bar.BarImage = memoryStream.ToArray();
                }
            }
            else
            {
                bar.BarImage = oldBar.BarImage;
            }
            if (msg == "")
            {
                int id = bar.Id;
                _context.Update(bar);
                await _context.SaveChangesAsync();
            }
         
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Details(int id)
        {
            ViewBag.userId = UserIdSupplier.id;
            var bar = _context.Bars.FirstOrDefault(y => y.Id == id);
            return View(bar);
        }


    }
}
