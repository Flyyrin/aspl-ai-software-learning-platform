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
            string sessionToken = Request.Cookies["sessionToken"];
            if (sessionToken != null)
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