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
        private readonly ChatLogic chatLogic;
        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger, IAuthenticationDataAccess authenticationDataAccess, ICourseDataAccess courseDataAccess, IStudentDataAccess studentDataAccess, ICodeDataAccess codeDataAccess, IChatDataAccess chatDataAccess)
        {
            authenticationLogic = new AuthenticationLogic(authenticationDataAccess);
            courseLogic = new CourseLogic(courseDataAccess);
            studentLogic = new StudentLogic(studentDataAccess);
            codeLogic = new CodeLogic(codeDataAccess);
            chatLogic = new ChatLogic(chatDataAccess);
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

                ViewBag.chapterMenu = TempData["chapterMenu"] ?? "closed";
                return View(appViewModel);
            }
            else
            {
                Response.Cookies.Delete("sessionToken");
                return Redirect("login");
            }
        }

        [Route("editor/{chapter:int}")]
        public IActionResult Edit(int chapter)
        {
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (sessionToken != null && authenticated)
            {
                Student student = studentLogic.GetStudentInfo(id);
                if (student.Role == "admin")
                {
                    Chapter selectedChapter = courseLogic.GetChapter(chapter);
                    var editViewModel = new EditViewModel
                    {
                        SelectedChapter = selectedChapter,
                    };

                    return View(editViewModel);
                } 
                else
                {
                    return Redirect("/");
                }
                
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

        public IActionResult GetChapter(int chapter)
        {
           Chapter chapterItem = courseLogic.GetChapter(chapter);
            return Json(chapterItem);
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

        [HttpPost]
        public IActionResult SaveAvatar(string avatar)
        {
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (authenticated)
            {
                bool success = studentLogic.SaveAvatar(id, avatar);
                return Content("{status:+ " + success + "}");
            }
            return Content("No Access");
        }

        public IActionResult getChat(int course)
        {
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (authenticated)
            {
                Chat chat = chatLogic.GetChat(id, course);
                return Json(chat);
            }
            return Content("No Access");
        }

        [HttpPost]
        public IActionResult SaveChat(int course, List<Dictionary<string, string>> chat)
        {
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (authenticated)
            {
                bool success = chatLogic.SaveChat(id, course, chat);
                return Content("{status:+ " + success + "}");
            }
            return Content("No Access");
        }

        [HttpPost]
        public IActionResult updateChapterDetails(int chapter, string name, string description)
        {
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (authenticated)
            {
                Student student = studentLogic.GetStudentInfo(id);
                if (student.Role == "admin")
                {
                    bool success = courseLogic.updateChapterDetails(chapter, name, description);
                    return Content("{status:+ " + success + "}");
                } 
                else
                {
                    return Content("No Access");
                }
            }
            return Content("No Access");
        }

        [Route("deleteChapter/{chapter:int}")]
        public IActionResult deleteChapter(int chapter)
        {
            Console.WriteLine(chapter);
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (authenticated)
            {
                Student student = studentLogic.GetStudentInfo(id);
                if (student.Role == "admin")
                {
                    bool success = courseLogic.deleteChapter(chapter);
                    TempData["chapterMenu"] = "open2";
                    return Redirect("/");
                }
                else
                {
                    return Content("No Access");
                }
            }
            return Content("No Access");
        }

        [Route("addChapter/{course:int}")]
        public IActionResult addChapter(int course)
        {
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (authenticated)
            {
                Student student = studentLogic.GetStudentInfo(id);
                if (student.Role == "admin")
                {
                    bool success = courseLogic.addChapter(course);
                    TempData["chapterMenu"] = "open";
                    return Redirect("/");
                }
                else
                {
                    return Content("No Access");
                }
            }
            return Content("No Access");
        }

        [HttpPost]
        public IActionResult saveContent(int chapter, string code)
        {
            string sessionToken = Request.Cookies["sessionToken"];
            authenticationLogic.AuthenticateUser(sessionToken, out bool authenticated, out int id);
            if (authenticated)
            {
                Student student = studentLogic.GetStudentInfo(id);
                if (student.Role == "admin")
                {
                    bool success = courseLogic.updateChapterContent(chapter, code);
                    return Content("{status:+ " + success + "}");
                }
                else
                {
                    return Content("No Access");
                }
            }
            return Content("No Access");
        }
    }
}