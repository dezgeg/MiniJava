using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Frontend.Trees
{
	public class WhileStmtTree : StmtTree
	{
		public ExprTree Condition { get; protected set; }
		public StmtTree Body { get; protected set; }
		public WhileStmtTree(Location loc, ExprTree condition, StmtTree body) : base(loc)
		{
			Condition = condition;
			Body = body;
		}
		public override string ToString()
		{
			return String.Format("While<{0}>: {{{1}}}", Condition, Body);
		}
		public override TStmtRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitStmt(this);
		}

	}
}
