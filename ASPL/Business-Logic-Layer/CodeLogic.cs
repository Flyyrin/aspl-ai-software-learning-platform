using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Business_Logic_Layer
{
    public class CodeLogic
    {
        private readonly ICodeDataAccess _codeDataAccess;

        public CodeLogic(ICodeDataAccess codeDataAccess)
        {
            _codeDataAccess = codeDataAccess;
        }

        public StudentCode GetCode(int student, int chapter)
        {
            DataTable result = _codeDataAccess.GetCode(student, chapter);
            if (result.Rows.Count != 0)
            {
                return new StudentCode(result.Rows[0]["code"].ToString(), result.Rows[0]["output"].ToString(), result.Rows[0]["error_explanation"].ToString());
            }
            return new StudentCode("", "", "");
        }

        public bool SaveCode(int student, int chapter, string code)
        {
            int rowsAffected = _codeDataAccess.SaveCode(student, chapter, code);
            return rowsAffected >= 1;
        }
    }
}