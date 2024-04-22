using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Models;
using System.Data;

namespace Business_Logic_Layer
{
    public class ChatLogic
    {
        private readonly IChatDataAccess _chatDataAccess;
        public ChatLogic(IChatDataAccess chatDataAccess)
        {
            _chatDataAccess = chatDataAccess;
        }

        public Chat GetChat(int student, int course)
        {
            int chatId = _chatDataAccess.getChatId(student, course);
            List<Message> chat = new List<Message>();

            DataTable result = _chatDataAccess.GetChat(chatId);
            if (result.Rows.Count != 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    string time = ((DateTime)row["time"]).ToString("yyyy-MM-dd HH:mm:ss");
                    chat.Add(new Message(row["sender"].ToString(), row["content"].ToString(), time));
                }
            }
            return new Chat(chat);
        }

        public bool SaveChat(int student, int course, List<Dictionary<string, string>> chat)
        {
            int rowsAffected = 0;
            int chatId = _chatDataAccess.getChatId(student, course);

            foreach (var message in chat)
            {
                string sender = message["sender"];
                string content = message["content"];
                string time = message["time"];

                rowsAffected += SaveMessage(chatId, sender, content, time);
            }

            return rowsAffected >= 1;
        }

        public int SaveMessage(int chat, string sender, string content, string time)
        {
            content = content.Replace("'", "''");
            int rowsAffected = _chatDataAccess.SaveMessage(chat, sender, content, time);
            return rowsAffected;
        }
    }
}