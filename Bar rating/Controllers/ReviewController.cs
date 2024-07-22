

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Resources;
using Bar_rating.Models;
using Bar_rating.Validator;

namespace Bar_rating.Controllers
{
    public class ReviewController:Controller
    {
        private readonly BarRatingDBContext _context;

        public ReviewController(BarRatingDBContext context)
        {
            _context=context;
        }

        public IActionResult Index(int id)
        {
            ViewBag.role = RoleSupplier.role;
            ViewBag.id = UserIdSupplier.id;
            if(id!=0)
            {
                BarIdSupplier.Id = id;
            }

            ViewBag.userId = UserIdSupplier.id;
            ViewBag.barId = BarIdSupplier.Id;
            
            if(BarIdSupplier.Id!=0)
            {
                return View(_context.Reviews.Where(p => p.BarId == BarIdSupplier.Id).ToList());
            }
            else
            {
                if(RoleSupplier.role=="Admin")
                {
                    return View(_context.Reviews.ToList());
                }
                else
                {
                    return View(_context.Reviews.Where(p => p.UserId == UserIdSupplier.id));
                }
            }


        }
       
        public IActionResult Create()
        {

            ViewBag.userId = UserIdSupplier.id;
            ViewBag.restaurantId = BarIdSupplier.Id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review)
        {
            review.BarId = BarIdSupplier.Id;
            review.UserId= UserIdSupplier.id;
            List<Review> reviews = _context.Reviews.ToList();
            
            if(reviews.Count==0)
            {
                review.Id = 1;
            }
            else
            {
                review.Id = reviews[reviews.Count() - 1].Id + 1;
            }

        
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = review.BarId });
        }


        public IActionResult Edit(int id) 
        {
            ViewBag.userId = UserIdSupplier.id;
            ViewBag.restaurantId = BarIdSupplier.Id;

            var reviews =_context.Reviews.AsNoTracking().FirstOrDefault(y => y.Id == id);
            return View(reviews);
        }
        public IActionResult Delete(int id)
        {
            ViewBag.userId = UserIdSupplier.id;
            ViewBag.restaurantId = BarIdSupplier.Id;

            var reviews = _context.Reviews.FirstOrDefault(x => x.Id == id);
            return View(reviews);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", new { id = review.BarId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Review review)
        {
           
            
               review.UserId=UserIdSupplier.id;
                int id = review.Id;
                _context.Update(review);
                await _context.SaveChangesAsync();


            return RedirectToAction("Index", new { id = review.BarId });

        }

        public  IActionResult Details(int id)
        {
            ViewBag.userId = UserIdSupplier.id;
            ViewBag.barId = BarIdSupplier.Id;

            var review = _context.Reviews.FirstOrDefault(y => y.Id == id);
            return View(review);
        }
      
    }
}
