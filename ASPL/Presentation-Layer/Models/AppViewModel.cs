using Business_Logic_Layer.Models;
using System.Globalization;

namespace Presentation_Layer.Models
{
    public class AppViewModel
    {
        public List<Course> Courses { get; set; }
        public Student Student {  get; set; }
        public int LastCourse { get; set; }
    }
}
