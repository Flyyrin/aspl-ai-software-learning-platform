using System.Data;
using Business_Logic_Layer.Interfaces;

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
    }
}