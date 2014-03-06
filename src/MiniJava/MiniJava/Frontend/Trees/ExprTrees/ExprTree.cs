using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Frontend.Trees
{
	public abstract class ExprTree : Tree
	{
		protected ExprTree(Location loc) : base(loc) { }
		public abstract TExprRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv);
		public virtual bool IsLValueExpr { get { return false; } }
		/// <summary>
		/// Type of this expression. This is calculated in semantic check phase.
		/// The code generator needs this in a few places, such as array stores and loads
		/// </summary>
		public JavaType Type { get; set; }
	}
	public abstract class LValueExprTree : ExprTree
	{
		public LValueExprTree(Location loc) : base(loc) { }
		public override bool IsLValueExpr { get { return true; } }
	}
	public abstract class LiteralExprTree : ExprTree
	{
		public LiteralExprTree(Location loc) : base(loc) { }
	}
}
