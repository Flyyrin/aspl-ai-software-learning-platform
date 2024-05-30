using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface ICourseDataAccess
    {
        DataTable GetCourses();
        DataTable GetChapters(int course);
        int updateChapterDetails(int chapter, string name, string description);
        int addChapter(int course);
        int deleteChapter(int chapter);
        int updateChapterContent(int chapter, string content);
        DataTable GetChapter(int chapter);
    }
}
