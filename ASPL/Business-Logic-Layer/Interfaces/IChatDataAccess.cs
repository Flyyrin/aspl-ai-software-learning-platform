using System.Data;

namespace Business_Logic_Layer.Interfaces
{
    public interface IChatDataAccess
    {
        int getChatId(int student, int course);
        DataTable GetChat(int chat);
        int SaveMessage(int chat, string sender, string content, string time);
    }
}