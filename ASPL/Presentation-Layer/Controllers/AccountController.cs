using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Models;
using System.Diagnostics;

namespace Presentation_Layer.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            ViewBag.wrongCredentials = TempData["wrongCredentials"] ?? false;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin")
            {
               return Redirect("/");
            }
            else
            {
                TempData["wrongCredentials"] = true;
                return Redirect("/login");
            }
        }

        public IActionResult Register()
        {
            ViewBag.usernameMessage = TempData["usernameMessage"] ?? "";
            ViewBag.emailMessage = TempData["emailMessage"] ?? "";
            ViewBag.passwordMessage = TempData["passwordMessage"] ?? "";
            ViewBag.emailTaken = TempData["emailTaken"] ?? "";
            ViewBag.passwordRepeatMessage = TempData["passwordRepeatMessage"] ?? "";
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string email, string password, string passwordRepeat)
        {
            if (username == "admin")
            {
                return Redirect("/");
            }
            else
            {
                TempData["usernameMessage"] = "Invalid Username. / Username Already Taken.";
                TempData["emailMessage"] = "Invalid Email./ Email Already Taken.";
                TempData["passwordMessage"] = "Invalid Password.";
                TempData["passwordRepeatMessage"] = "Passwords Do Not Match.";
                return Redirect("/register");
            }
        }
    }
}
