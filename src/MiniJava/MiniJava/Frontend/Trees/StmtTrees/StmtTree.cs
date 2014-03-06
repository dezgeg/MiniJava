using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Frontend.Trees
{
	public abstract class StmtTree : Tree
	{
		protected StmtTree(Location loc) : base(loc) { }
		public abstract TStmtRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv);
	}
	public class StmtListTree : StmtTree
	{
		public IList<StmtTree> Statements { get; protected set; }
		public StmtListTree(Location loc) : base(loc)
		{
			Statements = new List<StmtTree>();
		}
		public override string ToString()
		{
			return "[" + String.Join(", ", Statements) + "]";
		}
		public override TStmtRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitStmtList(this);
		}
	}
	public class ExprStmtTree : StmtTree
	{
		public ExprTree Expr { get; protected set; }
		public ExprStmtTree(Location loc, ExprTree expr)
			: base(loc)
		{
			Expr = expr;
		}
		public override string ToString()
		{
			return Expr.ToString();
		}
		public override TStmtRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitStmt(this);
		}
	}
}
