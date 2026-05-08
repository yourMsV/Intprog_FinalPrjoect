using Intprog_FinalPrjoect.Data;
using Intprog_FinalPrjoect.Models.ViewModels;
using Intprog_FinalPrjoect.Models;
using Microsoft.AspNetCore.Mvc;

namespace Intprog_FinalPrjoect.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            // Check duplicate email
            if (_context.Users.Any(u => u.Email == vm.Email))
            {
                ModelState.AddModelError("Email", "An account with this email already exists.");
                return View(vm);
            }

            var user = new User
            {
                FirstName = vm.FirstName,
                MiddleName = vm.MiddleName,
                LastName = vm.LastName,
                Email = vm.Email,
                PhoneNumber = vm.PhoneNumber,
                Password = vm.Password,
                Balance = 0
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            TempData["Success"] = "Account created! Please log in.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = _context.Users.FirstOrDefault(u => u.Email == vm.Email && u.Password == vm.Password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                TempData["Success"] = $"Welcome back, {user.FirstName}!";
                return RedirectToAction("Dashboard", "Home");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View(vm);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "You have been logged out.";
            return RedirectToAction("Login");
        }
    }
}
