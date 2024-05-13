using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Models;
using System.Diagnostics;
using Business_Logic_Layer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Primitives;
using Business_Logic_Layer.Interfaces;
using Data_Access_Layer;

namespace Presentation_Layer.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AuthenticationLogic authenticationLogic;

        public AccountController(ILogger<AccountController> logger, IAuthenticationDataAccess authenticationDataAcces)
        {
            authenticationLogic = new AuthenticationLogic(authenticationDataAcces);
            _logger = logger;
        }

        public IActionResult Login()
        {
            string sessionToken = Request.Cookies["sessionToken"];
            if (sessionToken != null)
            {
                return Redirect("/");
            }

            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Console.Write("Presentation Layer -> : ");
                string token = authenticationLogic.LoginUser(model.Username, model.Password);
                if (token != "")
                {
                    Response.Cookies.Append("sessionToken", token, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddMinutes(120),
                        HttpOnly = true
                    });
                    return Redirect("/");
                }
            }

            TempData["ErrorMessage"] = "Invalid username or password.";
            return Redirect("/login");
        }

        public IActionResult Register()
        {
            ViewBag.username = TempData["username"] ?? "";
            ViewBag.email = TempData["email"] ?? "";
            ViewBag.password = TempData["password"] ?? "";
            ViewBag.usernameMessage = TempData["usernameMessage"] ?? "";
            ViewBag.emailMessage = TempData["emailMessage"] ?? "";
            ViewBag.passwordMessage = TempData["passwordMessage"] ?? "";
            ViewBag.emailTaken = TempData["emailTaken"] ?? "";
            ViewBag.passwordRepeatMessage = TempData["passwordRepeatMessage"] ?? "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.PasswordRepeat)
                {
                    TempData["passwordRepeatMessage"] = "Passwords Do Not Match.";
                }
                else
                {
                    TempData["username"] = model.Username;
                    TempData["email"] = model.Email;
                    TempData["password"] = model.Password;
                    authenticationLogic.RegisterUser(model.Username, model.Email, model.Password, out string token, out bool usernameTaken, out bool emailTaken);

                    if (usernameTaken) {
                        TempData["usernameMessage"] = "Username Already Taken.";
                    }
                    if (emailTaken) {
                        TempData["emailMessage"] = "Email Already Taken.";
                    }
                    if (token != "")
                    {
                        Response.Cookies.Append("sessionToken", token, new CookieOptions
                        {
                            Expires = DateTimeOffset.UtcNow.AddMinutes(120),
                            HttpOnly = true
                        });
                        return Redirect("/");
                    }
                }
            }

            return Redirect("/register");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("sessionToken");
            return Redirect("/login");
        }
    }
}
