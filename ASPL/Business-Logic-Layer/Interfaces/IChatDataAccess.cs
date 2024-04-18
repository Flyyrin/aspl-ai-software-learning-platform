using System.Data;

namespace Business_Logic_Layer.Interfaces
{
    public interface IChatDataAccess
    {
        int getChatId(int student, int course);
        int SaveMessage(int chat, string sender, string content);
    }
}