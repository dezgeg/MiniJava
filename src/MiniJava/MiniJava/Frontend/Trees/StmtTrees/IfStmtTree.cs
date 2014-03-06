using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Frontend.Trees
{
	public class IfStmtTree : StmtTree
	{
		public ExprTree Condition { get; protected set; }
		public StmtTree Then { get; protected set; }
		public StmtTree Else { get; protected set; }
		public IfStmtTree(Location location, ExprTree condition, StmtTree then, StmtTree else_) : base(location)
		{
			this.Condition = condition;
			this.Then = then;
			this.Else = else_;
		}
		public override string ToString()
		{
			if(Else == null)
				return String.Format("If<{0}>: {{{1}}}", Condition, Then);
			else
				return String.Format("If<{0}>: {{{1}}}, else: {{{2}}}", Condition, Then, Else);
		}
		public override TStmtRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitStmt(this);
		}

	}
}
