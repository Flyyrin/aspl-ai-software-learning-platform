namespace Business_Logic_Layer.Models
{
    public class Course
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Desciption { get; private set; }
        public string Icon { get; private set; }

        public Course(int id, string name, string desciption, string icon)
        {
            Id = id;
            Name = name;
            Desciption = desciption;
            Icon = icon;
        }
    }
}