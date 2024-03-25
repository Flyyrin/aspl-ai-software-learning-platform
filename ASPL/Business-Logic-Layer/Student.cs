namespace Business_Logic_Layer
{
    public class Student
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }

        public Student(int id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
        }
    }
}