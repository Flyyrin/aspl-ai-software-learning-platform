using System.Data;

namespace Data_Access_Layer
{
    public class CourseDataAccess
    {
        private DataAccess dataAccess;

        public CourseDataAccess()
        {
            dataAccess = new DataAccess();
        }

        public DataTable GetCourses()
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM courses");
            return data;
        }

        public DataTable GetChapters(int course)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM chapters WHERE course_id = {course}");
            return data;
        }
    }
}