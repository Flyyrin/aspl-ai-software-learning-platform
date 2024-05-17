using Business_Logic_Layer;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Models;
using Moq;

namespace Unit_Tests
{
	[TestFixture]
	public class AiTests
	{
		private AiLogic aiLogic;

		[SetUp]
		public void Setup()
		{
			aiLogic = new AiLogic();
		}

		[Test]
		public async Task AskNameTest()
		{
			aiLogic.AddUserContent("whats your name?");
			string response = await aiLogic.MakeRequest();
			Assert.IsTrue(response.Contains("Garry"), $"Response: {response}");
		}

		[Test]
		public async Task RememberTest()
		{
			aiLogic.AddUserContent("My name is Student68382");
			aiLogic.AddModelContent("Oke i will remember.");
			aiLogic.AddUserContent("Whats my name?");
			string response = await aiLogic.MakeRequest();
			Assert.IsTrue(response.Contains("Student68382"), $"Response: {response}");
		}

		[Test]
		public async Task ContextTest()
		{
			aiLogic.AddUserContent("Who are you?");
			string response = await aiLogic.MakeRequest();
			Console.WriteLine(response);
			Assert.IsTrue(response.Contains("online software learing platform"), $"Response: {response}");
		}

		[Test]
		public async Task ErrorExplainationTest1()
		{
			string code = "print('test)";

			string response = await aiLogic.getErrorExplanation(code, "error");
			Console.WriteLine(response);
			Assert.IsNotEmpty(response, $"Response: {response}");
		}

		[Test]
		public async Task ErrorExplainationTest2()
		{
			string code = "print(test)";

			string response = await aiLogic.getErrorExplanation(code, "error");
			Console.WriteLine(response);
			Assert.IsNotEmpty(response, $"Response: {response}");
		}

		[Test]
		public async Task ErrorExplainationTest3()
		{
			string code = "Hello";

			string response = await aiLogic.getErrorExplanation(code, "Not printing");
			Console.WriteLine(response);
			Assert.IsNotEmpty(response, $"Response: {response}");
		}

		[Test]
		public async Task ErrorExplainationTest4()
		{
			string code = "console.log('test')";

			string response = await aiLogic.getErrorExplanation(code, "error");
			Console.WriteLine(response);
			Assert.IsNotEmpty(response, $"Response: {response}");
		}

		[Test]
		public async Task ErrorExplainationTest5()
		{
			string code = "('test')";

			string response = await aiLogic.getErrorExplanation(code, "doesnt print");
			Console.WriteLine(response);
			Assert.IsNotEmpty(response, $"Response: {response}");
		}
	}
}