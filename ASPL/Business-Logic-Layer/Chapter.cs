namespace Business_Logic_Layer
{
    public class Chapter
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Desciption { get; private set; }


        public Chapter(int id, string name, string desciption)
        {
            Id = id;
            Name = name;
            Desciption = desciption;
        }
    }
}
