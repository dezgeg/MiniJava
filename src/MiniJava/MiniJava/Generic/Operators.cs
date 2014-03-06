using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava
{
	public enum Binop
	{
		Assign,
		Or,
		And,
		EQ,
		NE,
		GE,
		LE,
		GT,
		LT,
		Plus,
		Minus,
		Times,
		Over,
		Mod,
	}
	public enum Unop
	{
		Minus,
		Not
	}
	public static class BinopHelpers
	{
		private static Dictionary<Binop, string> BinopStringTable;
		static BinopHelpers()
		{
			BinopStringTable = new Dictionary<Binop, string>();
			BinopStringTable[Binop.Assign] = "=";
			BinopStringTable[Binop.Or] = "||";
			BinopStringTable[Binop.And] = "&&";
			BinopStringTable[Binop.EQ] = "==";
			BinopStringTable[Binop.NE] = "!=";
			BinopStringTable[Binop.GE] = ">=";
			BinopStringTable[Binop.LE] = "<=";
			BinopStringTable[Binop.GT] = ">";
			BinopStringTable[Binop.LT] = "<";
			BinopStringTable[Binop.Plus] = "+";
			BinopStringTable[Binop.Minus] = "-";
			BinopStringTable[Binop.Times] = "*";
			BinopStringTable[Binop.Over] = "/";
			BinopStringTable[Binop.Mod] = "%";
		}
		public static string ToOperator(this Binop op)
		{
			return BinopStringTable[op];
		}
	}
}
