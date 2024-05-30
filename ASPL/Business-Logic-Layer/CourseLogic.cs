using System.Data;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Models;

namespace Business_Logic_Layer
{
    public class CourseLogic
    {
        private readonly ICourseDataAccess _courseDataAccess;
        public CourseLogic(ICourseDataAccess courseDataAccess) {
            _courseDataAccess = courseDataAccess;
        }

        public List<Course> GetCourses()
        {
            List<Course> courses = new List<Course>();

            DataTable result = _courseDataAccess.GetCourses();
            foreach (DataRow row in result.Rows)
            {
                courses.Add(new Course(Convert.ToInt32(row["id"]), row["name"].ToString(), row["description"].ToString(), row["icon"].ToString()));
            }

            return courses;
        }

        public List<Chapter> GetChapters(int course)
        {
            List<Chapter> chapters = new List<Chapter>();

            DataTable result = _courseDataAccess.GetChapters(course);
            foreach (DataRow row in result.Rows)
            {
                chapters.Add(new Chapter(Convert.ToInt32(row["id"]), row["name"].ToString(), row["description"].ToString(), ""));
            }

            return chapters;
        }

        public Chapter GetChapter(int chapter)
        {
            DataTable result = _courseDataAccess.GetChapter(chapter);
            if (result.Rows.Count > 0)
            {
                return new Chapter(Convert.ToInt32(result.Rows[0]["id"]), result.Rows[0]["name"].ToString(), result.Rows[0]["description"].ToString(), result.Rows[0]["content"].ToString());
            }

            return new Chapter(0, "Error", "Error", "<text>Error</text>");
        }

        public bool updateChapterDetails(int chapter, string name, string description)
        {
            int rowsAffected = _courseDataAccess.updateChapterDetails(chapter, name, description);
            return rowsAffected > 0;
        }

        public bool addChapter(int course)
        {
            int rowsAffected = _courseDataAccess.addChapter(course);
            return rowsAffected > 0;
        }

        public bool deleteChapter(int chapter)
        {
            int rowsAffected = _courseDataAccess.deleteChapter(chapter);
            return rowsAffected > 0;
        }

        public bool updateChapterContent(int chapter, string content)
        {
            int rowsAffected = _courseDataAccess.updateChapterContent(chapter, content);
            return rowsAffected > 0;
        }
        

    }
}