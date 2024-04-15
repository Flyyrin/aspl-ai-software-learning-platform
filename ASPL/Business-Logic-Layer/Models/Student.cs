namespace Business_Logic_Layer.Models
{
    public class Student
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
        public string Avatar { get; private set; }

        public Student(int id, string username, string email, string role, string avatar)
        {
            Id = id;
            Username = username;
            Email = email;
            Role = role;
            Avatar = avatar;
        }
    }
}