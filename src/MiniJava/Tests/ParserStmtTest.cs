using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MiniJava.Frontend;
using MiniJava.Frontend.Trees;

namespace Tests
{
	[TestClass()]
	public class ParserStmtTest
	{
		public static String ParseStmt(String stmt)
		{
			string code = "class Test { public int test() { " + stmt + " } }";
			Scanner sc = new Scanner();
			sc.SetSource(code, 0);
			Parser p = new Parser(sc);
			Assert.IsTrue(p.Parse());
			var meth = p.Classes["Test"].Methods["test"];
			return meth.Body.Statements[0].ToString();
		}

		[TestMethod()]
		public void ParserTestWhile()
		{
			String t = ParseStmt("while(count < 100) { count = count + 1; }");
			Assert.AreEqual("While<Binop<LT>(Var:count, Lit<100>)>: {[Binop<Assign>(Var:count, Binop<Plus>(Var:count, Lit<1>))]}", t);
		}
		[TestMethod()]
		public void ParserTestIfPrecedence()
		{
			String t = ParseStmt(
				"if(false)\n" +
				"    if(false)\n" +
				"        assert(false);\n" +
				"    else\n" +
				"        assert(false);\n");
			Assert.AreEqual("If<Lit<False>>: {If<Lit<False>>: {Assert(Lit<False>)}, else: {Assert(Lit<False>)}}", t);
		}

		[TestMethod()]
		public void ParserTestNullStatements()
		{
			String t = ParseStmt("{ while(true); if(crashed) { } }");
			Assert.AreEqual("[While<Lit<True>>: {[]}, If<Var:crashed>: {[]}]", t);
		}
		[TestMethod()]
		public void ParserTestPrints()
		{
			String t = ParseStmt("{ System.out.println(); System.out.println(1 > 0); }");
			Assert.AreEqual("[Print(), Print(Binop<GT>(Lit<1>, Lit<0>))]", t);
		}
		[TestMethod()]
		public void ParserTestDeclarations()
		{
			String t = ParseStmt("{ int[] is; boolean asd; Foo foo; }");
			Assert.AreEqual("[Decl<int[]>: is, Decl<boolean>: asd, Decl<Foo>: foo]", t);
		}
		[TestMethod()]
		public void ParserTestExprStatements()
		{
			String t = ParseStmt("{ " +
				"foo = 1;" +
				"zyx.zap();" +
				"bar[foo] = this.quux = false;" +
				"return bar;" +
			"}");
			Assert.AreEqual("[Binop<Assign>(Var:foo, Lit<1>), Call<zap: Var:zyx>(), Binop<Assign>(Index(Var:bar, Var:foo), Binop<Assign>(Field<quux>(This), Lit<False>)), Return(Var:bar)]", t);
		}
	}
}