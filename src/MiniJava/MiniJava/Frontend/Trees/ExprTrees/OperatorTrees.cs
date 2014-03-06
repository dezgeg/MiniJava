using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Frontend;

namespace MiniJava.Frontend.Trees
{
	public class BinopTree : ExprTree
	{
		public Binop Operator { get; protected set; }
		public ExprTree Lhs { get; protected set; }
		public ExprTree Rhs { get; protected set; }
		public BinopTree(Location loc, Binop op, ExprTree lhs, ExprTree rhs) : base(loc)
		{
			this.Operator = op;
			this.Lhs = lhs;
			this.Rhs = rhs;
		}
		public override string ToString()
		{
			return String.Format("Binop<{0}>({1}, {2})", this.Operator, this.Lhs, this.Rhs);
		}
		public override TExprRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitExpr(this);
		}
	}

	public class UnopTree : ExprTree
	{
		public Unop Operator { get; protected set; }
		public ExprTree Operand { get; protected set; }

		/* Why do we have this crap here? Well, consider int minVal = -2147483648, evilVal = 2147483648
		 * We want to accept the minVal declaration and reject the evilVal.
		 * But we cannot lex negative literals as single tokens, since then i = i-1; would fail.
		 */
		public static ExprTree Create(Location loc, Unop op, ExprTree operand)
		{
			if (op == Unop.Minus && operand is LiteralValueTree)
			{
				LiteralValueTree litTree = (LiteralValueTree)operand;
				if (litTree.Value is Int64)
					return new LiteralValueTree(loc.Merge(litTree.Location), -((long)litTree.Value));
			}
			return new UnopTree(loc, op, operand);
		}
		// Use UnopTree.Create to create new instances
		private UnopTree(Location loc, Unop op, ExprTree operand) : base(loc)
		{
			this.Operator = op;
			this.Operand = operand;
		}
		public override string ToString()
		{
			return String.Format("Unop<{0}>({1})", this.Operator, this.Operand);
		}
		public override TExprRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitExpr(this);
		}
	}
}
