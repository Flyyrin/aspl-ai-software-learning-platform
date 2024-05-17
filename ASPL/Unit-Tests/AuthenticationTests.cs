using Business_Logic_Layer;
using Business_Logic_Layer.Interfaces;
using Data_Access_Layer;

namespace Unit_Tests
{
	[TestFixture]
	public class AuthenticationTests
	{
		private AuthenticationLogic authenticationLogic;
		private IAuthenticationDataAccess realAuthDataAccess;

		[SetUp]
		public void Setup()
		{
			realAuthDataAccess = new AuthenticationDataAccess();
			string connectionString = "Server=localhost;Database=aspl_testing;Uid=root;Pwd=root;Charset=utf8mb4;";

			// Set the connection string on the real data access object
			realAuthDataAccess.SetConnectionString(connectionString);

			// Pass the real data access object to AuthenticationLogic
			authenticationLogic = new AuthenticationLogic(realAuthDataAccess);
		}

		[Test]
		public void LoginUserTest()
		{
			string username = "Rafael";
			string password = "@lol123";

			string token = authenticationLogic.LoginUser(username, password);
			Assert.IsNotEmpty(token, $"Authenticated {username}");
		}

		[Test]
		public void RegisterUserTest()
		{
			string username = "Rafael";
			string email = "mai123l@mail.com";
			string password = "@lol123";
			string avatar = "0-0-1";

			realAuthDataAccess.RegisterUser(username, email, password, avatar);

			Assert.IsNotEmpty(username, $"Registered {username}");
		}
	}
}
