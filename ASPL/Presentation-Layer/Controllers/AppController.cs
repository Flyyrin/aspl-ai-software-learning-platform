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
        private readonly CourseLogic courseLogic;
        private readonly StudentLogic studentLogic;
        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger, IAuthenticationDataAccess authenticationDataAccess, ICourseDataAccess courseDataAccess, IStudentDataAccess studentDataAccess)
        {
            authenticationLogic = new AuthenticationLogic(authenticationDataAccess);
            courseLogic = new CourseLogic(courseDataAccess);
            studentLogic = new StudentLogic(studentDataAccess);
            _logger = logger;
        }

        public IActionResult Index()
        {
            string sessionToken = Request.Cookies["sessionToken"];
            if (sessionToken != null && authenticationLogic.AuthenticateUser(sessionToken))
            {
                List<Course> courses = courseLogic.GetCourses();
                Student student = studentLogic.GetStudentInfo(0);

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
    }
}