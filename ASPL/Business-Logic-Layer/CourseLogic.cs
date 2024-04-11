using System.Data;

namespace Business_Logic_Layer
{
    public class CourseLogic
    {
        private readonly ICourseDataAccess courseDataAccess;
        public CourseLogic(ICourseDataAccess courseDataAccess) {
            courseDataAccess = courseDataAccess;
        }

        public List<Course> GetCourses()
        {
            List<Course> courses = new List<Course>();

            DataTable result = courseDataAccess.GetCourses();
            foreach (DataRow row in result.Rows)
            {
                courses.Add(new Course(Convert.ToInt32(row["id"]), row["name"].ToString(), row["description"].ToString(), row["icon"].ToString()));
            }

            return courses;
        }

        public List<Chapter> GetChapters(int course)
        {
            List<Chapter> chapters = new List<Chapter>();

            DataTable result = courseDataAccess.GetChapters(course);
            foreach (DataRow row in result.Rows)
            {
                chapters.Add(new Chapter(Convert.ToInt32(row["id"]), row["name"].ToString(), row["description"].ToString()));
            }

            return chapters;
        }
    }
}