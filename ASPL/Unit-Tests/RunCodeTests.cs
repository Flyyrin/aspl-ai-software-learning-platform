using Business_Logic_Layer;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Models;
using Data_Access_Layer;

namespace Unit_Tests
{
	[TestFixture]
	public class RunCodeTests
	{
		private CodeLogic codeLogic;
		private ICodeDataAccess realCodeDataAccess;

		[SetUp]
		public void Setup()
		{
			realCodeDataAccess = new CodeDataAccess();
			codeLogic = new CodeLogic(realCodeDataAccess);
		}

		[Test]
		public void PythonPrintTest()
		{
			string code = "print('Testing code 123')";

			StudentCode studentCode = codeLogic.RunCode(1, code);
			Console.WriteLine(studentCode.Output);
			Assert.IsTrue(studentCode.Output.Contains("Testing code 123"), $"Output: {studentCode.Output}");
		}

		[Test]
		public void PythonMathTest1()
		{
			string code = "x = 5\ny = 3\nprint(x+y)";

			StudentCode studentCode = codeLogic.RunCode(1, code);
			Console.WriteLine(studentCode.Output);
			Assert.IsTrue(studentCode.Output.Contains("8"), $"Output: {studentCode.Output}");
		}

		[Test]
		public void PythonMathTest2()
		{
			string code = "x = 15\ny = 5\nprint(x/y)";

			StudentCode studentCode = codeLogic.RunCode(1, code);
			Console.WriteLine(studentCode.Output);
			Assert.IsTrue(studentCode.Output.Contains("3"), $"Output: {studentCode.Output}");
		}

		[Test]
		public void PythonMathTest3()
		{
			string code = "x = 3\ny = 2\nprint(x**y)";

			StudentCode studentCode = codeLogic.RunCode(1, code);
			Console.WriteLine(studentCode.Output);
			Assert.IsTrue(studentCode.Output.Contains("9"), $"Output: {studentCode.Output}");
		}

		[Test]
		public void PythonMathTest4()
		{
			string code = "x = 30\ny = 5\nprint(30/5)";

			StudentCode studentCode = codeLogic.RunCode(1, code);
			Console.WriteLine(studentCode.Output);
			Assert.IsTrue(studentCode.Output.Contains("6"), $"Output: {studentCode.Output}");
		}
	}
}
