

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Resources;
using Bar_rating.Models;
using Bar_rating.Validator;
using Bar_rating.Suppliers;
using Bar_rating.Entities;
using Bar_rating.Services.Users;

namespace Bar_rating.Controllers
{
    public class UserController:Controller
    {
        private readonly BarRatingDBContext _context;
        private readonly IUsersService _usersService;

        public UserController(BarRatingDBContext context, IUsersService usersService)
        {
            _context=context;
            _usersService=usersService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.userId = UserIdSupplier.id;
            ViewBag.role = RoleSupplier.role;
            var users=await _usersService.GetUsersAsync();
            return View(users);  // Passing the list to the view
        }

        public IActionResult Create()
        {
            ViewBag.userId = UserIdSupplier.id;
            ViewBag.role = RoleSupplier.role;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            List<User> users = (List<User>)await _usersService.GetUsersAsync();


            string msg = "";

            UserValidator.AssignAdminRoleIfUserListIsEmpty(users, user);


            UserValidator.AddIdToUser(users, user);

            msg = UserValidator.ReturnErrorsCreate(users, user);
            ViewBag.Message = msg;

           if (msg=="")
            {
                await _usersService.CreateUserAsync(user);

                if(UserIdSupplier.id==0)
                {
                   return RedirectToAction("Login", "User");
                }
                else
                {
                    return RedirectToAction("Index","User");
                }
            }
            return View(user);
        }


        public IActionResult Login()
        {
            ViewBag.userId = UserIdSupplier.id;
            ViewBag.role= RoleSupplier.role;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginEntity partUser)
        {
            string msg = $"Няма такъв потребител!";

            List<User> users = (List<User>)await _usersService.GetUsersAsync();
              
            User user = users.FirstOrDefault(e => e.Username == partUser.Username && e.Password == partUser.Password);

            if(user!=null)
            {
                UserIdSupplier.id = user.Id;
                RoleSupplier.GetRole(user);
                msg = "Успешно влизане! Вашата роля е "+RoleSupplier.role;

                return RedirectToAction("Index", "Bar");
            }
            ViewBag.Message = msg;
            return View();
        }
        public IActionResult Logout()
        {
            RoleSupplier.role = "Empty";
            UserIdSupplier.id = 0;
            return View();
        }
       

        public IActionResult Edit(int id) 
        {
            ViewBag.userId = UserIdSupplier.id;
            var users =_context.Users.AsNoTracking().FirstOrDefault(y => y.Id == id);
            return View(users);
        }
        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.userId = UserIdSupplier.id;
            var users =await _usersService.GetUserByIdAsync(id);
            return View(users);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _usersService.ExistsById(id))
            {
                await _usersService.DeleteUserByIdAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            User oldUser = _context.Users.AsNoTracking().FirstOrDefault(p => p.Id == user.Id);
            List<User> users = _context.Users.AsNoTracking().ToList();
            
            string msg = "";
            msg = UserValidator.ReturnErrorsEdit(users, oldUser, oldUser.Id); 
            ViewBag.Message = msg;

            if (msg == "" && user.Password.Length>=6)
            {
                await _usersService.UpdateUser(user);
            }

            return RedirectToAction(nameof(Index));   
        }
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.userId = UserIdSupplier.id;
            var user =await _usersService.GetUserByIdAsync(id);
            return View(user);
        }
      
    }
}
