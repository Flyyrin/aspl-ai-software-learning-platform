using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Models;
using System.Data;

namespace Business_Logic_Layer
{
    public class StudentLogic
    {
        private readonly IStudentDataAccess _studentDataAccess;
        public StudentLogic(IStudentDataAccess studentDataAccess)
        {
            _studentDataAccess = studentDataAccess;
        }

        public Student GetStudentInfo(int id)
        {
            DataTable result = _studentDataAccess.GetStudentInfo(id);
            Student student = new Student(id, result.Rows[0]["username"].ToString(), result.Rows[0]["email"].ToString(), result.Rows[0]["role"].ToString());

            return student;
        }
    }
}