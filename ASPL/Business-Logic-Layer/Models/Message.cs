namespace Business_Logic_Layer.Models
{
    public class Message
    {
        public string Sender { get; private set; }
        public string Content { get; private set; }
        public string Time { get; private set; }


        public Message(string sender, string content, string time)
        {
            Sender = sender;
            Content = content;
            Time = time;
        }
    }
}
