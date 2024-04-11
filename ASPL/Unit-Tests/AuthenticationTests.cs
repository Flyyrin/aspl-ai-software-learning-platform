using Business_Logic_Layer;
using Org.BouncyCastle.Asn1.Cms;

namespace Unit_Tests
{
    public class AuthenticationTests
    {
        private AuthenticationLogic authenticationLogic;

        [SetUp]
        public void Setup()
        {
            authenticationLogic = new AuthenticationLogic();
        }

        [Test]
        public void LoginUserTest()
        {
            string username = "Rafael";
            string password = "@lol123";

            string token = authenticationLogic.LoginUser(username, password);
            Assert.IsNotEmpty(token);
        }

        [Test]
        public void RegisterUserTest()
        {
            string username = "RafaelTest";
            string email = "mai123l@mail.com";
            string password = "@lol123";

            authenticationLogic.RegisterUser(username, email, password, out string token, out bool usenameTaken, out bool emailTaken);
            
            if (usenameTaken)
            {
                Assert.Fail("Username Taken");
            }

            if (emailTaken)
            {
                Assert.Fail("Email Taken");
            }

            Assert.IsNotEmpty(token);
        }

        [Test]
        public void AuthenticateUserTest()
        {
            string username = "Rafael";
            string password = "@lol123";

            string token = authenticationLogic.LoginUser(username, password);
            bool authenticated = authenticationLogic.AuthenticateUser(token);
            Assert.IsTrue(authenticated);
        }
    }
}