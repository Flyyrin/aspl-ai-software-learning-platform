using System.Data;
using Business_Logic_Layer;

namespace Data_Access_Layer
{
    public class StudentDataAccess : IStudentDataAccess
    {
        private DataAccess dataAccess;

        public StudentDataAccess()
        {
            dataAccess = new DataAccess();
        }
    }
}