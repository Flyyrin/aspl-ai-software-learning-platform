using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using System.Data;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Text;

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

        public bool SaveCode(int student, int chapter, string code, string output, string errorExplanation)
        {
            int rowsAffected = _codeDataAccess.SaveCode(student, chapter, code, output, errorExplanation);
            return rowsAffected >= 1;
        }

        public StudentCode RunCode(int course, string code)
        {
            string output = $"Error {course}";
            if (course == 1) { //python

                var engine = Python.CreateEngine();
                try
                {
                    var outputStream = new MemoryStream();
                    var errorStream = new MemoryStream();
                    engine.Runtime.IO.SetOutput(outputStream, Console.Out);
                    engine.Runtime.IO.SetErrorOutput(errorStream, Console.Error);
                    engine.Execute(code);
                    outputStream.Seek(0, SeekOrigin.Begin);
                    errorStream.Seek(0, SeekOrigin.Begin);
                    output = new StreamReader(outputStream).ReadToEnd();
                    Console.WriteLine(output);
                    var error = new StreamReader(errorStream).ReadToEnd();
                    if (string.IsNullOrWhiteSpace(output))
                    {
                        output = "error:Code has no output";
                    } else
                    {
                        output = "success:" + output;
                    }
                }
                catch (Exception ex)
                {
                    output = "error:"+ ex.Message;
                }

            }
            else if (course == 2)
            {
                //C#
            } else if (course == 3)
            {
                //js
            }


            return new StudentCode(code, CorrectOutput(output), "");
        }

        private string CorrectOutput(string output)
        {
            output = output.Replace("'", "\"");
            return output;
        }

    }
}