using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Frontend.Trees
{
	public class LiteralValueTree : LiteralExprTree
	{
		public static readonly Type[] ALLOWED_TYPES = { typeof(int), typeof(bool) };
		public Object Value { get; set; }
		// see UnopTree for an explanation why int literals are represented as longs...
		public LiteralValueTree(Location loc, long val) : base(loc)
		{
			Value = val;
			Type = JavaType.Int;
		}
		public LiteralValueTree(Location loc, bool val) : base(loc)
		{
			Value = val;
			Type = JavaType.Bool;
		}
		public override string ToString()
		{
			return String.Format("Lit<{0}>", Value);
		}
		// TODO: does not really belong here
		public string ToCilLiteral()
		{
			if (Type.Equals(JavaType.Bool))
				return ((bool)Value) ? "ldc.i4.1" : "ldc.i4.0";
			return "ldc.i4 " + Value;
		}
		public override TExprRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitExpr(this);
		}
	}
	public class ThisLiteralTree : LiteralExprTree
	{
		public ThisLiteralTree(Location loc)
			: base(loc) { }
		public override string ToString()
		{
			return "This";
		}
		public override TExprRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitExpr(this);
		}
	}
	public class NullLiteralTree : LiteralExprTree
	{
		public NullLiteralTree(Location loc)
			: base(loc) { }
		public override string ToString()
		{
			return "Null";
		}
		public override TExprRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitExpr(this);
		}
	}
}
