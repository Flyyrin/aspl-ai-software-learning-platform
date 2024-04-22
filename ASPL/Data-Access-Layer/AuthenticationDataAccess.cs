using System.Data;
using Business_Logic_Layer.Interfaces;

namespace Data_Access_Layer
{
    public class AuthenticationDataAccess: IAuthenticationDataAccess
    {
        private DataAccess dataAccess;

        public AuthenticationDataAccess()
        {
            dataAccess = new DataAccess();
        }

        public bool CheckIfUsernameExists(string username)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT username FROM students WHERE username = '{username}'");
            return data.Rows.Count == 1;
        }

        public bool CheckIfEmailExists(string email)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT email FROM students WHERE email = '{email}'");
            return data.Rows.Count == 1;
        }

        public DataTable LoginUser(string username, string password)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT password, id FROM students WHERE Username = '{username}'");
            return data;
        }

        public int RegisterUser(string username, string email, string password, string avatar)
        {
            int rowsAffected = dataAccess.ExecuteNonQuery($"INSERT INTO students (username, email, password, avatar) VALUES ('{username}', '{email}', '{password}', '{avatar}')");
            return rowsAffected;
        }
    }
}