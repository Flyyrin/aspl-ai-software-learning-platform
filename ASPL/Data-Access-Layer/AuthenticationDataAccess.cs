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
            Console.Write("usernameTaken: ");
            Console.WriteLine(data.Rows.Count == 1);
            Console.WriteLine(data);
            return data.Rows.Count == 1;
        }

        public bool CheckIfEmailExists(string email)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT email FROM students WHERE email = '{email}'");
            Console.Write("emailTaken: ");
            Console.WriteLine(data.Rows.Count == 1);
            Console.WriteLine(data);
            return data.Rows.Count == 1;
        }

        public DataTable LoginUser(string username, string password)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT password, id FROM students WHERE Username = '{username}'");
            Console.Write("Data Access Layer -> : ");
            Console.WriteLine("Working");
            return data;
        }

        public int RegisterUser(string username, string email, string password)
        {
            int rowsAffected = dataAccess.ExecuteNonQuery($"INSERT INTO students (username, email, password) VALUES ('{username}', '{email}', '{password}')");
            return rowsAffected;
        }
    }
}