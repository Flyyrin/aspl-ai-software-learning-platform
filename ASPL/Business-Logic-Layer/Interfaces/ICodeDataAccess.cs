using System.Data;

namespace Business_Logic_Layer.Interfaces
{
    public interface ICodeDataAccess
    {
       DataTable GetCode(int student, int chapter);
       int SaveCode(int student, int chapter, string code);
    }
}