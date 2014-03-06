using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniJava.Frontend;
using System.IO;
using QUT.Gppg;

namespace Tests
{
	[TestClass]
	public class ScannerTest
	{
		public static Scanner Scan(String content, String filename = "ScannerTest.mj")
		{
			Scanner sc = new Scanner();
			sc.SetSource(content, 0);
			return sc;
		}
		[TestMethod()]
		public void ScannerConstructorTest()
		{
			Scanner sc = Scan("12345");
			Assert.AreEqual(Tokens.IntLiteral, (Tokens)sc.yylex());
			Assert.AreEqual(12345, sc.yylval.IntValue);
			Assert.AreEqual(new Location(1, 0, 1, 5), sc.yylloc);

			Assert.AreEqual(Tokens.EOF, (Tokens)sc.yylex());
		}
		[TestMethod()]
		public void ScannerConstructorTestTwoTokens()
		{
			Scanner sc = Scan("AsdBar \t ==");
			Assert.AreEqual(Tokens.Identifier, (Tokens)sc.yylex());
			Assert.AreEqual("AsdBar", sc.yylval.StringValue);
			Assert.AreEqual(new Location(1, 0, 1, 6), sc.yylloc);

			Assert.AreEqual(Tokens.EQ, (Tokens)sc.yylex());
			Assert.AreEqual(new Location(1, 9, 1, 11), sc.yylloc);

			Assert.AreEqual(Tokens.EOF, (Tokens)sc.yylex());

		}
		[TestMethod()]
		public void ScannerTestComment()
		{
			Scanner sc = Scan("/* //d**d */ if");
			Assert.AreEqual(Tokens.If, (Tokens)sc.yylex());
			Assert.AreEqual(new Location(1, 13, 1, 15), sc.yylloc);

			Assert.AreEqual(Tokens.EOF, (Tokens)sc.yylex());
		}
		[TestMethod()]
		public void ScannerTestNestedComment()
		{
			Scanner sc = Scan("/*/asd /****\nasd */ *****/ while");
			Assert.AreEqual(Tokens.While, (Tokens)sc.yylex());
			Assert.AreEqual(new Location(2, 14, 2, 19), sc.yylloc);

			Assert.AreEqual(Tokens.EOF, (Tokens)sc.yylex());
		}
		[TestMethod()]
		public void ScannerTestEolComment()
		{
			Scanner sc = Scan("//* asdas \r\n  {  // zdzdzdzzz");
			Assert.AreEqual((int)'{', sc.yylex());
			Assert.AreEqual(new Location(2, 2, 2, 3), sc.yylloc);

			Assert.AreEqual(Tokens.EOF, (Tokens)sc.yylex());
		}
		[TestMethod()]
		public void ScannerTestNewline()
		{
			Scanner sc = Scan("     \n [    ]");

			Assert.AreEqual(Tokens.DIM, (Tokens)sc.yylex());
			Assert.AreEqual(new Location(2, 1, 2, 7), sc.yylloc);

			Assert.AreEqual(Tokens.EOF, (Tokens)sc.yylex());
		}
#if false
		[TestMethod()]
		public void ScannerTestStringLiteral()
		{
			Scanner sc = Scan("\"abcde\"");
			Token tok = sc.NextToken();
			Assert.AreEqual(new Token(Tokens.StringLiteral, new Location(1, 0, 0, 7), "abcde"), tok);
		}
		[TestMethod()]
		public void ScannerTestStringLiteralEscapes()
		{
			Scanner sc = Scan("\"ab\\\\d\\\"e\"");
			Token tok = sc.NextToken();
			Assert.AreEqual(new Token(Tokens.StringLiteral, new Location(1, 0, 0, 10), "ab\\d\"e"), tok);
		}
#endif
		[TestMethod()]
		public void ScannerTestOperators()
		{
			Scanner sc = Scan("<= < >= > == != ! || && + - * / % = , ; . ( [ ) ] { } [ ]");
			String expected = "LE < GE > EQ NE ! OR AND + - * / % = , ; . ( [ ) ] { } DIM";	
			List<String> actuals = new List<String>();
			bool eof = false;
			while(!eof)
			{
				try {
					Tokens actual = (Tokens)sc.yylex();
					if (actual == Tokens.EOF)
						break;
					if (Enum.IsDefined(typeof(Tokens), actual))
						actuals.Add(actual.ToString());
					else
						actuals.Add(((char)actual).ToString());
				} catch (Exception)
				{
					break;
				}
			}
			Assert.AreEqual(expected, String.Join(" ", actuals));
		}
		[TestMethod()]
		public void ScannerTestKeywords()
		{
			{ }
			Scanner sc = Scan("assert boolean class extends else false if int length main new "
			+ "null out println public return static System this true void while");
			Tokens[] expected = { Tokens.Assert, Tokens.Boolean, Tokens.Class, Tokens.Extends, Tokens.Else,
									Tokens.False, Tokens.If, Tokens.Int, Tokens.Length, Tokens.Main, Tokens.New,
									Tokens.Null, Tokens.Out, Tokens.PrintLn, Tokens.Public, Tokens.Return,
									Tokens.Static, Tokens.System, Tokens.This, Tokens.True, Tokens.Void, Tokens.While
			};
			List<Tokens> actuals = new List<Tokens>();
			bool eof = false;
			while (!eof)
			{
				try
				{
					Tokens actual = (Tokens)sc.yylex();
					if (actual == Tokens.EOF)
						break;
					actuals.Add(actual);
				}
				catch (Exception)
				{
					break;
				}
			}
			Assert.AreEqual(String.Join(", ", expected), String.Join(", ", actuals));
		}
	}

}
