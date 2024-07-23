

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Resources;
using Bar_rating.Models;
using Bar_rating.Validator;
using Bar_rating.Suppliers;
using Bar_rating.Services.Reviews;

namespace Bar_rating.Controllers
{
    public class ReviewController:Controller
    {
        private readonly BarRatingDBContext _context;
        private readonly IReviewsService _reviewService;

        public ReviewController(BarRatingDBContext context, IReviewsService reviewService)
        {
            _context=context;
            _reviewService=reviewService;
        }

        public async Task<IActionResult> Index(int id)
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
                    return View(await _reviewService.GetReviewsAsync());
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
            List<Review> reviews = (List<Review>)await _reviewService.GetReviewsAsync();
            
            if(reviews.Count==0)
            {
                review.Id = 1;
            }
            else
            {
                review.Id = reviews[reviews.Count() - 1].Id + 1;
            }

        
            await _reviewService.CreateReviewAsync(review);

            //Chance for error
            return RedirectToAction("Index", new { id = review.BarId });
        }


        public IActionResult Edit(int id) 
        {
            ViewBag.userId = UserIdSupplier.id;
            ViewBag.restaurantId = BarIdSupplier.Id;

            var reviews =_context.Reviews.AsNoTracking().FirstOrDefault(y => y.Id == id);
            return View(reviews);
        }
        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.userId = UserIdSupplier.id;
            ViewBag.restaurantId = BarIdSupplier.Id;

            var reviews = await _reviewService.GetReviewByIdAsync(id);
            return View(reviews);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review != null)
            {
                await _reviewService.DeleteReviewByIdAsync(id);
            }

            return RedirectToAction("Index", new { id = review.BarId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Review review)
        {
           
            
               review.UserId=UserIdSupplier.id;
                int id = review.Id;

            await _reviewService.UpdateReviewAsync(review);


            return RedirectToAction("Index", new { id = review.BarId });

        }

        public  async Task<IActionResult> Details(int id)
        {
            ViewBag.userId = UserIdSupplier.id;
            ViewBag.barId = BarIdSupplier.Id;

            var review = await _reviewService.GetReviewByIdAsync(id);
            return View(review);
        }
      
    }
}
