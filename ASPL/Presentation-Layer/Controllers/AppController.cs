using Business_Logic_Layer;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Models;
using System.Diagnostics;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Models;

namespace Presentation_Layer.Controllers
{
    public class AppController : Controller
    {
        private readonly AuthenticationLogic authenticationLogic;
        private readonly CourseLogic courseLogic;
        private readonly StudentLogic studentLogic;
        private readonly CodeLogic codeLogic;
        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger, IAuthenticationDataAccess authenticationDataAccess, ICourseDataAccess courseDataAccess, IStudentDataAccess studentDataAccess, ICodeDataAccess codeDataAccess)
        {
            authenticationLogic = new AuthenticationLogic(authenticationDataAccess);
            courseLogic = new CourseLogic(courseDataAccess);
            studentLogic = new StudentLogic(studentDataAccess);
            codeLogic = new CodeLogic(codeDataAccess);
            _logger = logger;
        }

        public IActionResult Index()
        {
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (sessionToken != null && authenticated)
            {
                List<Course> courses = courseLogic.GetCourses();
                Student student = studentLogic.GetStudentInfo(id);

                var appViewModel = new AppViewModel
                {
                    Courses = courses,
                    Student = student,
                    LastCourse = Convert.ToInt32(Request.Cookies["lastCourse"] ?? "1"),
                };


                return View(appViewModel);
            }
            else
            {
                Response.Cookies.Delete("sessionToken");
                return Redirect("login");
            }
        }

        public IActionResult GetChapters(int course)
        {
            List<Chapter> chapters = courseLogic.GetChapters(course);
            return Json(chapters);
        }

        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult GetCode(int chapter)
        {
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (authenticated)
            {
                StudentCode studentCode = codeLogic.GetCode(id, chapter);
                return Json(studentCode);
            }
            return Content("No Access");
        }

        [HttpPost]
        public IActionResult SaveCode(int chapter, string code, string output, string errorExplanation)
        {
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (authenticated)
            {
                bool success = codeLogic.SaveCode(id, chapter, code, output, errorExplanation);
                return Content("{status:+ "+success+"}");
            }
            return Content("No Access");
        }

        [HttpPost]
        public IActionResult RunCode(int course, string code)
        {
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (authenticated)
            {
                StudentCode studentCode = codeLogic.RunCode(course, code);
                return Content(studentCode.Output);
            }
            return Content("No Access");
        }
    }
}