using BaseProjectMvc_Net8.Models;
using BaseProjectMvc_Net8.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BaseProjectMvc_Net8.Controllers
{
    public class AccountController(IAccountService accountService) : Controller
    {
        private readonly IAccountService _accountService = accountService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var response = await _accountService.ValidateUser(loginModel);

            if (response is null)
            {
                return View("Register");
            }

            HttpContext.Session.SetString("UserID", response.Email!);

            return View("Index", response);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            await _accountService.RegisterUser(registerModel);

            return View("Login");
        }

    }
}
