using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Frontend.Trees
{
	public class VariableTree : LValueExprTree
	{
		public String Name { get; protected set; }
		public VariableBinding Binding { get; set; }
		public VariableTree(Location loc, String variable) : base(loc)
		{
			this.Name = variable;
		}
		public override string ToString()
		{
			return "Var:" + this.Name;
		}
		public override TExprRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitExpr(this);
		}
	}
	public class ArrayLengthExprTree : ExprTree
	{
		public ExprTree Expr { get; protected set; }
		public ArrayLengthExprTree(Location loc, ExprTree expr)
			: base(loc)
		{
			Expr = expr;
		}
		public override string ToString()
		{
			return "Length(" + Expr.ToString() + ")";
		}
		public override TExprRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitExpr(this);
		}
	}
	public class ArrayAccessExprTree : LValueExprTree
	{
		public ExprTree ArrayExpr { get; protected set; }
		public ExprTree IndexExpr { get; protected set; }
		public ArrayAccessExprTree(Location loc, ExprTree arrayExpr, ExprTree indexExpr) : base(loc)
		{
			ArrayExpr = arrayExpr;
			IndexExpr = indexExpr;
		}
		public override string ToString()
		{
			return "Index(" + ArrayExpr.ToString() + ", " + IndexExpr.ToString() + ")";
		}
		public override TExprRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitExpr(this);
		}
	}
	public class FieldAccessExprTree : LValueExprTree
	{
		public FieldBinding Binding { get; set; }
		public ExprTree Expr { get; protected set; }
		public String Field { get; protected set; }
		public FieldAccessExprTree(Location loc, ExprTree expr, String field)
			: base(loc)
		{
			Expr = expr;
			Field = field;
		}
		public override string ToString()
		{
			return String.Format("Field<{0}>({1})", Field, Expr);
		}
		public override TExprRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitExpr(this);
		}
	}

}
