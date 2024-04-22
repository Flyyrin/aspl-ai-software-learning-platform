namespace Business_Logic_Layer.Models
{
    public class Chat
    {
        public List<Message> Messages { get; private set; }

        public Chat(List<Message> messages)
        {
            Messages = messages;
        }
    }
}
