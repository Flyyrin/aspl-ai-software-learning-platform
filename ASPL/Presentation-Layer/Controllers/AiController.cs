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
        private readonly AiLogic aiLogic;
        private readonly ILogger<AppController> _logger;

        public AiController(ILogger<AppController> logger, IAuthenticationDataAccess authenticationDataAccess)
        {
            authenticationLogic = new AuthenticationLogic(authenticationDataAccess);
            aiLogic = new AiLogic();
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> getErrorExplanation(string code, string error)
        {
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (authenticated)
            {
                return Content(await aiLogic.getErrorExplanation(code, error));
            }
            return Content("No Access");
        }
    }
}