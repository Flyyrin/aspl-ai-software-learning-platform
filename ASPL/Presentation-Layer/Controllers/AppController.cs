using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Models;
using System.Diagnostics;

namespace Presentation_Layer.Controllers
{
    public class AppController : Controller
    {
        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies.TryGetValue("session_token", out string sessionToken))
            {
                Console.WriteLine(sessionToken);
                return View();
            }
            else
            {
                return Redirect("login");
            }
        }

        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}