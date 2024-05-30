using System.Data;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Models;

namespace Data_Access_Layer
{
    public class CourseDataAccess: ICourseDataAccess
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

        public DataTable GetChapter(int chapter)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM chapters WHERE id = {chapter}");
            return data;
        }

        public int updateChapterDetails(int chapter, string name, string description)
        {
            int rowsAffected = dataAccess.ExecuteNonQuery($"UPDATE chapters SET name = '{name}', description = '{description}' WHERE id = {chapter};");
            return rowsAffected;
        }

        public int addChapter(int course)
        {
            int rowsAffected = dataAccess.ExecuteNonQuery($"INSERT INTO chapters (name, description, course_id) VALUES ('New chapter', 'New chapter description', {course});");
            return rowsAffected;
        }

        public int deleteChapter(int chapter)
        {
            int rowsAffected = dataAccess.ExecuteNonQuery($"DELETE FROM chapters WHERE id = {chapter};");
            return rowsAffected;
        }

        public int updateChapterContent(int chapter, string content)
        {
            int rowsAffected = dataAccess.ExecuteNonQuery($"UPDATE chapters SET content = '{content}' WHERE id = {chapter};");
            return rowsAffected;
        }
    }
}