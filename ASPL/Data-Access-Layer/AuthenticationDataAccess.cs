using System.Data;

namespace Data_Access_Layer
{
    public class AuthenticationDataAccess
    {
        private DataAccess dataAccess;

        public AuthenticationDataAccess()
        {
            dataAccess = new DataAccess();
        }

        public DataTable LoginUser(string username, string password)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT password, id FROM students WHERE Username = '{username}'");
            return data;
        }
    }
}