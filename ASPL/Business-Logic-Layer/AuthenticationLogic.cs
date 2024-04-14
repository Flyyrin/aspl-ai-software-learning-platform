using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Business_Logic_Layer.Interfaces;

namespace Business_Logic_Layer
{
    public class AuthenticationLogic
    {
        private string jwtToken = "k5{+j3gSjSvHkLF$MGL)o)!,~V47_Q4.)t]_1H8^M13{o3S0ML";
        private string jwtIssuer = "ASPL";
        private readonly IAuthenticationDataAccess _authenticationDataAccess;
        public AuthenticationLogic(IAuthenticationDataAccess authenticationDataAccess)
        {
            _authenticationDataAccess = authenticationDataAccess;
        }

        public string LoginUser(string username, string password)
        {
            string token = "";
            Console.Write("Logic Layer -> : ");
            DataTable result = _authenticationDataAccess.LoginUser(username, password);
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

            usernameTaken = _authenticationDataAccess.CheckIfUsernameExists(username);
            emailTaken = _authenticationDataAccess.CheckIfEmailExists(email);

            if (!usernameTaken && !emailTaken)
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                int rowsAffected = _authenticationDataAccess.RegisterUser(username, email, passwordHash);
                if (rowsAffected == 1)
                {
                    token = LoginUser(username, password);

                }
            }
        }

        public void AuthenticateUser(string token, out bool authenticated, out int id)
        {
            authenticated = false;
            id = 0;
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

                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "id");
                    if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                    {
                        authenticated = true;
                        id = userId;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("error");
            }
        }
    }
}