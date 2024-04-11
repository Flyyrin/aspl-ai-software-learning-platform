using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public interface ICourseDataAccess
    {
        DataTable GetCourses();
        DataTable GetChapters(int course);
    }
}
