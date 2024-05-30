namespace Business_Logic_Layer.Models
{
    public class Chapter
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Content { get; private set; }


        public Chapter(int id, string name, string description, string content)
        {
            Id = id;
            Name = name;
            Description = description;
            Content = content;
        }
    }
}
