using Business_Logic_Layer;
using Business_Logic_Layer.Interfaces;
using IronPython.Compiler;
using Moq;
using System.Data;

namespace Unit_Tests
{
	[TestFixture]
	public class AuthenticationTests
	{
		private AuthenticationLogic authenticationLogic;
        private Mock<IAuthenticationDataAccess> mockAuthDataAccess;

        [SetUp]
		public void Setup()
		{
            mockAuthDataAccess = new Mock<IAuthenticationDataAccess>();
        }

		[Test]
		public void LoginUserTest()
		{
			mockAuthDataAccess.Setup(m => m.LoginUser("Rafael", "@lol123")).Returns(new DataTable() { Columns = { "Username", "Password", { "Id", typeof(int) } }, Rows = { { "Rafael", "$2a$10$cXQgIro/BHqK4EYOr5dOseMH.4wN/eWi.2JEoGLZoOuGF/Og0frYC", 0 } } });
            authenticationLogic = new AuthenticationLogic(mockAuthDataAccess.Object);

            string username = "Rafael";
			string password = "@lol123";

			string token = authenticationLogic.LoginUser(username, password);
			Assert.IsNotEmpty(token, $"Not authenticated {username}");
		}

        [Test]
		public void RegisterUserTest()
		{
            mockAuthDataAccess.Setup(m => m.RegisterUser("Rafael", "Rafael@mail.com", "@lol123", "1-0-0")).Returns(1);
            authenticationLogic = new AuthenticationLogic(mockAuthDataAccess.Object);

            string username = "Rafael";
			string email = "Rafael@mail.com";
			string password = "@lol123";

			authenticationLogic.RegisterUser(username, email, password, out string token, out bool usernameTaken, out bool emailTaken); token += ":id";

            Assert.IsNotEmpty(token, $"Not authenticated {username}");
			Assert.IsFalse(usernameTaken, $"{username} taken");
            Assert.IsFalse(emailTaken, $"{email} taken");
        }

		private delegate void RegisterUserCallback(string username, string email, string password, string avatar, out string token, out bool usernameTaken, out bool emailTaken);
	}
}
