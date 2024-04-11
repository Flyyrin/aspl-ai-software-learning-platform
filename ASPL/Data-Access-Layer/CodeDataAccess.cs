using System.Data;
using Business_Logic_Layer.Interfaces;

namespace Data_Access_Layer
{
    public class CodeDataAccess: ICodeDataAccess
    {
        private DataAccess dataAccess;

        public CodeDataAccess()
        {
            dataAccess = new DataAccess();
        }

        public DataTable GetCode(int student, int chapter)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM student_code WHERE student_id = {student} AND chapter_id = {chapter}");
            return data;
        }

        public int SaveCode(int student, int chapter, string code)
        {
            int rowsAffected = 0;
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM student_code WHERE student_id = {student} AND chapter_id = {chapter}");
            if (data.Rows.Count > 0) {
                Console.WriteLine("update");
                rowsAffected = dataAccess.ExecuteNonQuery($"UPDATE student_code SET code = '{code}' WHERE student_id = {student} AND chapter_id = {chapter};");
            }
            else
            {
                rowsAffected = dataAccess.ExecuteNonQuery($"INSERT INTO student_code (student_id, chapter_id, code) VALUES ({student}, {chapter}, '{code}');");
            }
            return rowsAffected;
        }
    }
}