using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using MiniJava.Frontend;
using MiniJava.Frontend.Trees;
using System.Collections.Generic;
using MiniJava;

namespace MiniLangTests
{
	[TestClass()]
	public class ParserLocationTest
	{
		[TestMethod()]
		public void ParserTestClasses()
		{
			Scanner sc = new Scanner();
			String content =
			//   0         10        20        30
			//   v    v    v    v    v    v    v
				"class Fibo {\r\n" +							//  1
				"    public static void main() {\r\n" +			//  2
				"        int f;\r\n" +							//  3
				"        f = new Fibo().calc(10);\r\n" +		//  4
				"        System.out.println(f);\r\n" +			//  5
				"    }\r\n" +									//  6
				"    public int calc(int n) {\r\n" +			//  7
				"        if (n <= 2)\r\n" +						//  8
				"            return 1;\r\n" +					//  9
				"        else\r\n" +                            // 10
				"            return this.calc(n - 1) +\r\n" +	// 11
				"            this.calc(n - 2);\r\n" +			// 12
				"    }\r\n" +									// 13
				"}\r\n";										// 14
			sc.SetSource(content, 0);
			Parser p = new Parser(sc);
			Assert.IsTrue(p.Parse());

			var cls = p.Classes["Fibo"];
			Assert.AreEqual(new Location(1, 0, 14, 1), cls.Location);

			var main = cls.Methods["main"];
			Assert.AreEqual(new Location(2, 4, 6, 5), main.Location);
			var calc = cls.Methods["calc"];
			Assert.AreEqual(new Location(7, 4, 13, 5), calc.Location);
			var ifStmt = (IfStmtTree)calc.Body.Statements[0];
			Assert.AreEqual(new Location(8, 8, 12, 29), ifStmt.Location);

			Assert.AreEqual(new Location(8, 12, 8, 18), ifStmt.Condition.Location);
			Assert.AreEqual(new Location(9, 12, 9, 21), ifStmt.Then.Location);
			Assert.AreEqual(new Location(11, 12, 12, 29), ifStmt.Else.Location);
		}

	}
}
