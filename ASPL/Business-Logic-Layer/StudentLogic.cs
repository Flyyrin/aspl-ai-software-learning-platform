namespace Business_Logic_Layer
{
    public class StudentLogic
    {
        private readonly ICourseDataAccess courseDataAccess;
        public StudentLogic(IStudentDataAccess studentDataAccess)
        {
            studentDataAccess = studentDataAccess;
        }

        public Student GetStudentInfo(int id)
        {
            Student student = new Student(id, "Rafael", "Rafael@mail.com");

            return student;
        }
    }
}