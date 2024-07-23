

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Resources;
using Bar_rating.Models;
using Bar_rating.Validator;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bar_rating.Suppliers;
using Bar_rating.Services.Bars;
using Bar_rating.Services.Reviews;

namespace Bar_rating.Controllers
{
    public class BarController:Controller
    {
        private readonly BarRatingDBContext _context;
        private readonly IBarsService _barsService;


        public BarController(BarRatingDBContext context, IBarsService barsService, IReviewsService reviewsService)
        {
            _context=context;
            _barsService=barsService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.userId = UserIdSupplier.id;
            ViewBag.role = RoleSupplier.role;
            return View(await _barsService.GetBarsAsync());
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

            List<Bar> bars = (List<Bar>)await _barsService.GetBarsAsync();

            if (bars.Count == 0)
            {
                bar.Id = 1;
            }
            else
            {
                bar.Id = bars[bars.Count() - 1].Id + 1;
            }

         
            msg = await BarValidator.ReturnErrorsCreateAsync(bars, bar);
            ViewBag.Message = msg;

            if (msg == "")
            {
                await _barsService.CreateBarAsync(bar);
                return RedirectToAction(nameof(Index));
            }
            return View(bar);
        }
     

        public IActionResult Edit(int id)
        {
            ViewBag.userId = UserIdSupplier.id;
            var bars = _context.Bars.AsNoTracking().FirstOrDefault(y => y.Id == id);
            return View(bars);
        }
        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.userId = UserIdSupplier.id;
            var bar =await _barsService.GetBarByIdAsync(id);
            return View(bar);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _barsService.ExistsById(id))
            {
               await _barsService.DeleteBarByIdAsync(id);
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
            msg = await BarValidator.ReturnErrorsEditAsync(bars, oldBar, oldBar.Id);
            ViewBag.Message = msg;

            await BarValidator.UpdateImage(oldBar, bar);
          

            if (msg == "")
            {
                int id = bar.Id;
                await _barsService.UpdateBar(bar);
            }
         
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.userId = UserIdSupplier.id;
            var bar = await _barsService.GetBarByIdAsync(id);
            return View(bar);
        }


    }
}
