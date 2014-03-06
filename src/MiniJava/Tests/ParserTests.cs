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
	public class ParserTests
	{
		public static IDictionary<string, ClassTree> Parse(String content, String filename = "ScannerTest.mj")
		{
			Scanner sc = new Scanner();
			sc.SetSource(content, 0);
			Parser p = new Parser(sc);
			Assert.IsTrue(p.Parse());
			return p.Classes;
		}

		[TestMethod()]
		public void ParserTestClasses()
		{
			var res = Parse("class Test\n{\n\n}\nclass Bar extends Foo {}");
			Assert.AreEqual(2, res.Count);
			Assert.IsTrue(res.ContainsKey("Test"));
			Assert.IsTrue(res.ContainsKey("Bar"));

			Assert.AreEqual(new Location(1, 0, 4, 1), res["Test"].Location);
			Assert.IsNull(res["Test"].SuperClassName);
			Assert.AreEqual("Foo", res["Bar"].SuperClassName);
		}

		[TestMethod()]
		public void ParserTestField()
		{
			var res = Parse("class Test { int x; Foo foo; } ");
			var fields = res["Test"].Fields;

			Assert.AreEqual(2, fields.Count);
			Assert.IsTrue(fields.ContainsKey("x"));
			Assert.IsTrue(fields.ContainsKey("foo"));

			var foo = fields["foo"];
			Assert.AreEqual(new JavaClassType("Foo"), foo.Type);
			Assert.AreEqual("foo", foo.Name);
			Assert.AreEqual(new Location(1, 20, 1, 28), foo.Location);

			var x = fields["x"];
			Assert.AreEqual(JavaType.Int, x.Type);
			Assert.AreEqual("x", x.Name);
			Assert.AreEqual(new Location(1, 13, 1, 19), x.Location);
		}
		[TestMethod()]
		public void ParserTestMethod()
		{
			var res = Parse("class Test { public int[] foo(boolean x, int a) {} } ");
			var meths = res["Test"].Methods;

			Assert.AreEqual(1, meths.Count);
			Assert.IsTrue(meths.ContainsKey("foo"));

			var foo = meths["foo"];
			Assert.AreEqual("foo", foo.Name);
			Assert.IsFalse(foo.IsMain);
			Assert.AreEqual(new Location(1, 13, 1, 50), foo.Location);
		}
		[TestMethod()]
		public void ParserTestMainMethod()
		{
			var res = Parse("class Test { public static void main() {} } ");
			var meths = res["Test"].Methods;

			Assert.AreEqual(1, meths.Count);
			Assert.IsTrue(meths.ContainsKey("main"));

			var main = meths["main"];
			Assert.AreEqual("main", main.Name);
			Assert.IsTrue(main.IsMain);
			Assert.AreEqual(new Location(1, 13, 1, 41), main.Location);
		}
		[TestMethod()]
		public void ParserTestMethodArgs()
		{
			var res = Parse("class Test { \n" +
				"public void zero(){}\n" +
				"public boolean one(Bar[] bar){}\n" +
				"public int two(A a, B b){}\n" +
				"}");
			var meths = res["Test"].Methods;

			Assert.AreEqual(3, meths.Count);
			Assert.AreEqual(0, meths["zero"].Arguments.Count);

			var args1 = meths["one"].Arguments;
			Assert.AreEqual(1, args1.Count);
			Assert.AreEqual("bar", args1.First().Var);
			Assert.AreEqual(new JavaArrayType(new JavaClassType("Bar")),
							 args1.First().Type);
			Assert.AreEqual(2, meths["two"].Arguments.Count);

		}
	}
}
