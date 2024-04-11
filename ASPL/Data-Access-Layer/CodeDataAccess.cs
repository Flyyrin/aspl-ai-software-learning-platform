using System.Data;

namespace Data_Access_Layer
{
    public class CodeDataAccess
    {
        private DataAccess dataAccess;

        public CodeDataAccess()
        {
            dataAccess = new DataAccess();
        }

        public DataTable GetCode(int student, int chapter)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT code FROM student_code WHERE student_id = {student}, chapter_id = {chapter}");
            return data;
        }

        public int SaveCode(int student, int chapter, string code)
        {
            int rowsAffected = dataAccess.ExecuteNonQuery($"INSERT INTO student_code (student_id, chapter_id, code) VALUES ({student}, {chapter}, '{code}') ON DUPLICATE KEY UPDATE code = VALUES(code);");
            return rowsAffected;
        }
    }
}