using System.Data;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Models;

namespace Data_Access_Layer
{
    public class StudentDataAccess : IStudentDataAccess
    {
        private DataAccess dataAccess;

        public StudentDataAccess()
        {
            dataAccess = new DataAccess();
        }

        public DataTable GetStudentInfo(int id)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM students WHERE id = {id}");
            return data;
        }

        public int SaveAvatar(int student, string avatar)
        {
            int rowsAffected = dataAccess.ExecuteNonQuery($"UPDATE students SET avatar = '{avatar}' WHERE id = {student};");
            return rowsAffected;
        }
    }
}