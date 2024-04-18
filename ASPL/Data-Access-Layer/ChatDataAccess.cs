using System.Data;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Models;

namespace Data_Access_Layer
{
    public class ChatDataAccess : IChatDataAccess
    {
        private DataAccess dataAccess;

        public ChatDataAccess()
        {
            dataAccess = new DataAccess();
        }

        public int getChatId(int student, int course)
        {
            int chat = 0;
            DataTable data = dataAccess.ExecuteQuery($"SELECT chat_id FROM student_chat WHERE student_id = {student} AND course_id = {course}");
            if (data.Rows.Count > 0)
            {
                chat = Convert.ToInt32(data.Rows[0]["chat_id"]);
            }
            else
            {
                object id = dataAccess.ExecuteScalarQuery($"INSERT INTO student_chat (student_id, course_id) VALUES ({student}, {course}); SELECT LAST_INSERT_ID();");
                chat = Convert.ToInt32(id);
            }

            return chat;
        }

        public int SaveMessage(int chat, string sender, string content)
        {
            Console.WriteLine(content);
            int rowsAffected = dataAccess.ExecuteNonQuery($"INSERT IGNORE INTO messages (chat_id, sender, content) VALUES ({chat}, '{sender}', '{content}');");
            return rowsAffected;
        }
    }
}