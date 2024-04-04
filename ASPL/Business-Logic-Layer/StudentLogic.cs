using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class StudentLogic
    {
        private readonly CourseDataAccess courseDataAccess;
        public StudentLogic()
        {
            courseDataAccess = new CourseDataAccess();
        }

        public Student GetStudentInfo(int id)
        {
            Student student = new Student(id, "Rafael", "Rafael@mail.com");

            return student;
        }
    }
}