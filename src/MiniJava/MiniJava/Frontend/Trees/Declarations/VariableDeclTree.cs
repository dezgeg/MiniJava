using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Frontend.Trees
{
	/// <summary>
	/// Represents a local variable, method argument declaration in the parse tree.
	/// Additionally, currently represents a field declaration in the parser.
	/// </summary>
	public class VariableDeclTree : StmtTree
	{
		public String Var { get; set; }
		public JavaType Type { get; protected set; }
		public RegisterBinding Binding { get; set; }

		public VariableDeclTree(Location loc, JavaType type, String variable) : base(loc)
		{
			this.Var = variable;
			this.Type = type;
		}
		public override string ToString()
		{
			return String.Format("Decl<{0}>: {1}", this.Type, this.Var);
		}
		public override TStmtRet Accept<TExprRet, TStmtRet>(ITreeVisitor<TExprRet, TStmtRet> itv)
		{
			return itv.visitStmt(this);
		}

	}
}
