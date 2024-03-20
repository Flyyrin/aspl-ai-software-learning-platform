using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class CourseLogic
    {
        private readonly CourseDataAccess courseDataAccess;
        public CourseLogic() {
            courseDataAccess = new CourseDataAccess();
        }
    }
}