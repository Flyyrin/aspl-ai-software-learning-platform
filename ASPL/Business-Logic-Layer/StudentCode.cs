namespace Business_Logic_Layer
{
    public class StudentCode
    {
        public string Code { get; private set; }
        public string Output { get; private set; }
        public string ErrorExplanation { get; private set; }

        public StudentCode(string code, string output, string errorExplanation)
        {
            Code = code;
            Output = output;
            ErrorExplanation = errorExplanation;
        }
    }
}
