using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Frontend.Trees
{
	public class MethodCallExprTree : ExprTree
	{
		public ExprTree Expr { get; protected set; }
		public String Method { get; protected set; }
		public LinkedList<ExprTree> Args { get; protected set; }
		/// <summary>
		/// Semantic check resolves this to the earliest declaration (i.e. not an override) of 
		/// </summary>
		public MethodTree ResolvedMethod { get; set; }
		public MethodCallExprTree(Location loc, ExprTree expr, String method, LinkedList<ExprTree> args)
			: base(loc)
		{
			Expr = expr;
			Method = method;
			Args = args;
		}
		public override string ToString()
		{
			return String.Format("Call<{1}: {0}>({2})", Expr, Method, String.Join(", ", Args));
		}
		public override TExprRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitExpr(this);
		}
	}
	public class NewExprTree : ExprTree
	{
		public JavaType ObjectType { get; protected set; }

		// if "new Foo()", then ArrayLengthExpr == null
		// if "new Foo[10]" then ArrayLengthExpr == Lit<Int>:10
		public ExprTree ArrayLengthExpr { get; protected set; }
		public NewExprTree(Location loc, JavaType type, ExprTree arrayExpr)
			: base(loc)
		{
			ObjectType = type;
			ArrayLengthExpr = arrayExpr;
		}
		public override string ToString()
		{
			if (ArrayLengthExpr == null)
				return String.Format("New<{0}>()", ObjectType);
			else
				return String.Format("New<{0}[]>({1})", ObjectType, ArrayLengthExpr);
		}
		public override TExprRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitExpr(this);
		}
	}
}
