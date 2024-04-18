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

        public bool SaveChat(int student, int course, List<Dictionary<string, string>> chat)
        {
            int rowsAffected = 0;
            int chatId = _chatDataAccess.getChatId(student, course);

            foreach (var message in chat)
            {
                string sender = message["sender"];
                string content = message["content"];

                rowsAffected += SaveMessage(chatId, sender, content);
            }

            return rowsAffected >= 1;
        }

        public int SaveMessage(int chat, string sender, string content)
        {
            content = content.Replace("'", "''");
            int rowsAffected = _chatDataAccess.SaveMessage(chat, sender, content);
            return rowsAffected;
        }
    }
}