using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MiniJava.Frontend;
using MiniJava.Frontend.Trees;

namespace Tests
{
	[TestClass()]
	public class ParserExprTest
	{
		public static String ParseExpr(String expr)
		{
			string code = "class Test { public int test() { return " + expr + "; } }";
			Scanner sc = new Scanner();
			sc.SetSource(code, 0);
			Parser p = new Parser(sc);
			Assert.IsTrue(p.Parse());
			var meth = p.Classes["Test"].Methods["test"];
			return ((ReturnStmtTree)meth.Body.Statements[0]).Expr.ToString();
		}

		[TestMethod()]
		public void ParserTestComplexExpression()
		{
			Assert.AreEqual("Binop<Or>(Binop<Or>(Binop<GT>(Lit<1>, Lit<0>), Binop<NE>(This, Null)), Binop<EQ>(Lit<False>, Lit<True>))", 
				ParseExpr("1 > 0 || this != null || false == true"));
		}
		[TestMethod()]
		public void ParserTestParenExpression()
		{
			String t = ParseExpr("(1 + 2) * 3");
			Assert.AreEqual("Binop<Times>(Binop<Plus>(Lit<1>, Lit<2>), Lit<3>)", t);
		}
		[TestMethod()]
		public void ParserTestAssociativity()
		{
			String t = ParseExpr("3 - 2 - 1");
			Assert.AreEqual("Binop<Minus>(Binop<Minus>(Lit<3>, Lit<2>), Lit<1>)", t);
		}
		[TestMethod()]
		public void ParserTestUnaryExpression()
		{
			String t = ParseExpr("!a && bc");
			Assert.AreEqual("Binop<And>(Unop<Not>(Var:a), Var:bc)", t);
		}
		[TestMethod()]
		public void ParserTestMultipleBinops()
		{
			String t = ParseExpr("1 + 2 * -a / -4");
			Assert.AreEqual("Binop<Plus>(Lit<1>, Binop<Over>(Binop<Times>(Lit<2>, Unop<Minus>(Var:a)), Lit<-4>))", t);
		}
		[TestMethod()]
		public void ParserTestNew()
		{
			String t = ParseExpr("new int[10].length");
			Assert.AreEqual("Length(New<int[]>(Lit<10>))", t);
			String s = ParseExpr("new Biff().blaargh");
			Assert.AreEqual("Field<blaargh>(New<Biff>())", s);
		}
		[TestMethod()]
		public void ParserTestMethodCalls()
		{
			String t = ParseExpr("this.frobnicate(quux % 2)");
			Assert.AreEqual("Call<frobnicate: This>(Binop<Mod>(Var:quux, Lit<2>))", t);

			String s = ParseExpr("trol.lol().lol().lol()");
			Assert.AreEqual("Call<lol: Call<lol: Call<lol: Var:trol>()>()>()", s);

			String q = ParseExpr("quux[baz].zzyx(foo >= bar, -biif <= 0, new ZZ())");
			Assert.AreEqual("Call<zzyx: Index(Var:quux, Var:baz)>(Binop<GE>(Var:foo, Var:bar), Binop<LE>(Unop<Minus>(Var:biif), Lit<0>), New<ZZ>())", q);
		}
	}
}
