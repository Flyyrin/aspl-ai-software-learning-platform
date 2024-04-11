using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace Business_Logic_Layer
{
    public class AuthenticationLogic
    {
        private string jwtToken = "k5{+j3gSjSvHkLF$MGL)o)!,~V47_Q4.)t]_1H8^M13{o3S0ML";
        private string jwtIssuer = "ASPL";
        private readonly IAuthenticationDataAccess authenticationDataAccess;
        public AuthenticationLogic(IAuthenticationDataAccess authenticationDataAccess)
        {
            authenticationDataAccess = authenticationDataAccess;
        }

        public string LoginUser(string username, string password)
        {
            string token = "";
            DataTable result = authenticationDataAccess.LoginUser(username, password);
            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];
                int studentId = (int)row["id"];
                string passwordHash = (string)row["password"];
                bool verified = BCrypt.Net.BCrypt.Verify(password, passwordHash);
                if (verified)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("id", studentId.ToString())
                    };

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var Sectoken = new JwtSecurityToken(jwtIssuer,
                        jwtIssuer,
                        claims: claims,
                        null,
                        expires: DateTime.Now.AddMinutes(120),
                        signingCredentials: credentials);

                    token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
                }
            }

            return token;
        }

        public void RegisterUser(string username, string email, string password, out string token, out bool usernameTaken, out bool emailTaken)
        {
            usernameTaken = true;
            emailTaken = true;
            token = "";

            usernameTaken = authenticationDataAccess.CheckIfUsernameExists(username);
            emailTaken = authenticationDataAccess.CheckIfEmailExists(email);

            if (!usernameTaken && !emailTaken)
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                int rowsAffected = authenticationDataAccess.RegisterUser(username, email, passwordHash);
                Console.WriteLine(rowsAffected);
                if (rowsAffected == 1)
                {
                    token = LoginUser(username, password);

                }
            }
        }

        public bool AuthenticateUser(string token)
        {
            bool authenticated = false;
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken));

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "ASPL",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateAudience = false,
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);

                authenticated =  true;
            }
            catch (Exception)
            {
                Console.WriteLine("error");
            }
            return authenticated;
        }
    }
}