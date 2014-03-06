using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Frontend.Trees
{
	public class PrintStmtTree : StmtTree
	{
		public ExprTree Expr { get; protected set; }

		public PrintStmtTree(Location loc, ExprTree exprTree) : base(loc)
		{
			this.Expr = exprTree;
		}
		public override string ToString()
		{
			return "Print(" + ((object)Expr ?? "").ToString() + ")";
		}
		public override TStmtRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitStmt(this);
		}

	}
	public class AssertStmtTree : StmtTree
	{
		public ExprTree Expr { get; protected set; }

		public AssertStmtTree(Location loc, ExprTree exprTree) : base(loc)
		{
			this.Expr = exprTree;
		}
		public override string ToString()
		{
			return "Assert(" + Expr + ")";
		}
		public override TStmtRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitStmt(this);
		}
	}
	public class ReturnStmtTree : StmtTree
	{
		public ExprTree Expr { get; protected set; }

		public ReturnStmtTree(Location loc, ExprTree exprTree)
			: base(loc)
		{
			this.Expr = exprTree;
		}
		public override string ToString()
		{
			return "Return(" + Expr + ")";
		}
		public override TStmtRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitStmt(this);
		}
	}
}
