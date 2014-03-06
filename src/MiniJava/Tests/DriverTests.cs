using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using MiniJava.CLI;

namespace MiniJava
{
	[TestClass()]
	public class DriverTest
	{
		public static String ReadFile(string path, string basename, string ext)
		{
			String file = path + "\\" + basename + "." + ext;
			if (!File.Exists(file))
				return null;
			return File.ReadAllText(file);
		}
#if false
		[TestMethod()]
		public void DoCmdlineCodeTest()
		{
			StringWriter stdout = new StringWriter();
			Console.SetOut(stdout);

			int status = Program.Instance.RealMain(new String[] { "-e", "print 42;" });
			Assert.AreEqual(0, status);
			Assert.AreEqual("42", stdout.ToString());
		}
#endif
		[TestMethod()]
		public void DoCmdlineNonexistingFileTest()
		{
			StringWriter stderr = new StringWriter();
			Console.SetError(stderr);

			int status = Program.Instance.RealMain(new String[] { "asdasadasdasadasadasadasadsadasadsadasdasa" });
			Assert.AreEqual(2, status);
			Assert.IsTrue(stderr.ToString().Contains("Could not find file"));
		}
		[TestMethod()]
		public void DoCmdlineShowsUsageTest()
		{
			StringWriter stderr = new StringWriter();
			Console.SetError(stderr);

			int status = Program.Instance.RealMain(new String[] { });
			Assert.AreEqual(3, status);
			Assert.IsTrue(stderr.ToString().Contains("usage:"));
		}
		[TestMethod()]
		public void DoCmdlineExecExpr()
		{
			StringWriter stdout = new StringWriter();
			Console.SetOut(stdout);

			int status = Program.Instance.RealMain(new String[] { "-e", "1 + 41" });
			Assert.AreEqual(0, status);
			Assert.IsTrue(stdout.ToString().Contains("42"));
		}
		[TestMethod()]
		public void DoCmdlineExecStmt()
		{
			StringWriter stdout = new StringWriter();
			Console.SetOut(stdout);

			int status = Program.Instance.RealMain(new String[] { "-s", "int x; x = 42; System.out.println(x);" });
			Assert.AreEqual(0, status);
			Assert.IsTrue(stdout.ToString().Contains("42"));
		}
		[TestMethod()]
		public void DoCmdlineDumpCil()
		{
			StringWriter stderr = new StringWriter();
			Console.SetError(stderr);

			int status = Program.Instance.RealMain(new String[] { "-d", "-s", "int[] xs; xs = new int[10];" });
			Assert.AreEqual(0, status);
			Assert.IsTrue(stderr.ToString().Contains("newarr [mscorlib]System.Int32"));
		}

		[TestMethod()]
		public void DoDriverTests()
		{
			bool errors = false;
			string path = @"..\..\..\Tests\DriverTests";
			DirectoryInfo di = new DirectoryInfo(path);
			foreach (FileInfo fi in di.GetFiles("*.mj", SearchOption.AllDirectories))
			{
				string basename = fi.FullName.Substring(di.FullName.Length + 1).Replace(".mj", "");
				string expectedStdout = ReadFile(path, basename, "out");
				string expectedStderr = ReadFile(path, basename, "err");
				int expectedStatus;
				if (expectedStderr == null)
					expectedStatus = 0;
				else
					expectedStatus = expectedStdout == null ? 2 : 3;
				expectedStdout = expectedStdout ?? "";
				expectedStderr = expectedStderr ?? "";

				StringWriter stdout = new StringWriter();
				StringWriter stderr = new StringWriter();
				Console.SetOut(stdout);
				Console.SetError(stderr);
				int status = Program.Instance.RealMain(new String[] { "-t", path + "\\" + basename + ".mj" });
				if (expectedStderr != stderr.ToString())
				{
					errors = true;
					File.WriteAllText(path + "\\" + basename + ".err.ACTUAL", stderr.ToString());
				}
				if (expectedStdout != stdout.ToString())
				{
					errors = true;
					File.WriteAllText(path + "\\" + basename + ".out.ACTUAL", stdout.ToString());
				}
			}
			Assert.IsFalse(errors);
		}
	}
}
