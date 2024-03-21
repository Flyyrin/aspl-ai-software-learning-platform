using Data_Access_Layer;
using Org.BouncyCastle.Crypto.Generators;
using System.Data;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Security.Claims;

namespace Business_Logic_Layer
{
    public class AuthenticationLogic
    {
        private string jwtToken = "k5{+j3gSjSvHkLF$MGL)o)!,~V47_Q4.)t]_1H8^M13{o3S0ML";
        private string jwtIssuer = "ASPL";
        private readonly AuthenticationDataAccess authenticationDataAccess;
        public AuthenticationLogic()
        {
            authenticationDataAccess = new AuthenticationDataAccess();
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

        public bool AuthenticateUser(string username, string password)
        {
            return true;
        }
    }
}