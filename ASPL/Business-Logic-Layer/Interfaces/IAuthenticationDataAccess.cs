using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface IAuthenticationDataAccess
    {
        int RegisterUser(string username, string email, string password, string avatar);
        bool CheckIfUsernameExists(string username);
        bool CheckIfEmailExists(string email);
        DataTable LoginUser(string username, string password);
        void SetConnectionString(string connectionString);

	}
}