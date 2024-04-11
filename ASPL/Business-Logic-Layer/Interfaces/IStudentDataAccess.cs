using System.Data;

namespace Business_Logic_Layer.Interfaces
{
    public interface IStudentDataAccess
    {
        DataTable GetStudentInfo(int id);
    }
}