using Business_Logic_Layer;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Models;
using System.Diagnostics;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Models;

namespace Presentation_Layer.Controllers
{
    public class AiController : Controller
    {
        private readonly AuthenticationLogic authenticationLogic;
        private readonly ILogger<AppController> _logger;

        public AiController(ILogger<AppController> logger, IAuthenticationDataAccess authenticationDataAccess)
        {
            authenticationLogic = new AuthenticationLogic(authenticationDataAccess);
            _logger = logger;
        }

        [HttpPost]
        public IActionResult getErrorExplanation(int course, string code)
        {
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (authenticated)
            {
                return Content("error Explain test");
            }
            return Content("No Access");
        }
    }
}