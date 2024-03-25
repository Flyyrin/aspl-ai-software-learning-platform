using Business_Logic_Layer;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Models;
using System.Diagnostics;
using Business_Logic_Layer;

namespace Presentation_Layer.Controllers
{
    public class AppController : Controller
    {
        private readonly AuthenticationLogic authenticationLogic;
        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger)
        {
            authenticationLogic = new AuthenticationLogic();
            _logger = logger;
        }

        public IActionResult Index()
        {
            string sessionToken = Request.Cookies["sessionToken"];
            if (sessionToken != null && authenticationLogic.AuthenticateUser(sessionToken))
            {
                Console.WriteLine("verified");
                return View();
            }
            else
            {
                Response.Cookies.Delete("sessionToken");
                return Redirect("login");
            }
        }

        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}