using CoffeeShopBLL.Services.AuthService;
using CoffeeShopDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoffeeShopPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // Constructor receives services via Dependency Injection
        public AccountController(AuthService authService, SignInManager<ApplicationUser> signInManager)
        {
            _authService = authService;
            _signInManager = signInManager;
        }

        // ----- REGISTRATION -----
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, string password, string confirmPassword)
        {
            // Quick check in Presentation Tier
            if (password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }

            // Call Business Logic Tier
            var result = await _authService.RegisterUserAsync(username, email, password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login"); // Go to login page on success
            }

            // Add errors to display in view
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }

        // ----- LOGIN -----
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, bool rememberMe)
        {
            var result = await _authService.ValidateLoginAsync(username, password, rememberMe);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        // ----- LOGOUT -----
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}