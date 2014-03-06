using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Frontend;
using MiniJava.Frontend.Trees;

namespace MiniJava.Backend
{
	public partial class CodeGen : ITreeVisitor<bool, bool>
	{
		// These functions return bool values just because there is no way to use the void type in generics x_x
		public bool visitExpr(BinopTree tree)
		{
			if (tree.Operator == Binop.Assign)
			{
				emitAssign(tree);
				return true;
			}
			tree.Lhs.Accept(this);
			if (tree.Operator == Binop.And || tree.Operator == Binop.Or)
			{
				String skipLbl = MakeLabel();
				EmitInsn("dup");
				EmitInsn(tree.Operator == Binop.And ?
					"brfalse " + skipLbl : "brtrue " + skipLbl);
				EmitInsn("pop");
				tree.Rhs.Accept(this);
				EmitLabel(skipLbl);
				return true;
			}

			tree.Rhs.Accept(this);
			switch(tree.Operator)
			{
				case Binop.Over: EmitInsn("div"); break;
				case Binop.Minus: EmitInsn("sub"); break;
				case Binop.Plus: EmitInsn("add"); break;
				case Binop.Times: EmitInsn("mul"); break;
				case Binop.Mod: EmitInsn("rem"); break;

				case Binop.EQ: EmitInsn("ceq"); break;
				case Binop.NE: EmitInsn("ceq"); EmitNot(); break;
				case Binop.LT: EmitInsn("clt"); break;
				case Binop.LE: EmitInsn("cgt"); EmitNot(); break;
				case Binop.GT: EmitInsn("cgt"); break;
				case Binop.GE: EmitInsn("clt"); EmitNot(); break;
				default:
					throw new InvalidProgramException();
			}
			return true;
		}

		private void emitAssign(BinopTree tree)
		{
			VariableTree varTree = null;
			FieldAccessExprTree fieldTree = null;
			ArrayAccessExprTree arrayTree = null;
			if ((varTree = tree.Lhs as VariableTree) != null)
			{
				if (varTree.Binding is FieldBinding)
				{
					int temp = AllocScratchRegister(tree.Rhs.Type);
					EmitLoadThis();
					tree.Rhs.Accept(this);
					EmitInsn("dup");
					EmitInsn("stloc " + temp);
					EmitInsn("st" + varTree.Binding);
					EmitInsn("ldloc " + temp);
				}
				else
				{
					tree.Rhs.Accept(this);
					EmitInsn("dup");
					EmitInsn("st" + varTree.Binding);
				}

			}
			else if ((arrayTree = tree.Lhs as ArrayAccessExprTree) != null)
			{
				int temp = AllocScratchRegister(tree.Rhs.Type);
				arrayTree.ArrayExpr.Accept(this);
				arrayTree.IndexExpr.Accept(this);
				tree.Rhs.Accept(this);
				EmitInsn("dup");
				EmitInsn("stloc " + temp);
				EmitInsn("stelem." + tree.Type.CilTypePrefix);
				EmitInsn("ldloc " + temp);
			}
			else if ((fieldTree = tree.Lhs as FieldAccessExprTree) != null)
			{
				int temp = AllocScratchRegister(tree.Rhs.Type);

				fieldTree.Expr.Accept(this);
				tree.Rhs.Accept(this);
				EmitInsn("dup");
				EmitInsn("stloc " + temp);
				EmitInsn("st" + fieldTree.Binding);
				EmitInsn("ldloc " + temp);
			}
		}
		public bool visitExpr(LiteralValueTree tree)
		{
			EmitInsn(tree.ToCilLiteral());
			return true;
		}

		public bool visitExpr(UnopTree tree)
		{
			tree.Operand.Accept(this);
			switch (tree.Operator)
			{
				case Unop.Minus: EmitInsn("neg"); break;
				case Unop.Not: EmitNot(); break;
				default: throw new InvalidProgramException();
			}
			return true;
		}
		public bool visitExpr(VariableTree tree)
		{
			if (tree.Binding is FieldBinding)
				EmitLoadThis();
			EmitInsn("ld" + tree.Binding);
			return true;
		}

		public bool visitExpr(ThisLiteralTree thisLiteralTree)
		{
			EmitLoadThis();
			return true;
		}

		private void EmitLoadThis()
		{
			EmitInsn("ldarg.0");
		}

		public bool visitExpr(ArrayLengthExprTree arrayLengthExprTree)
		{
			arrayLengthExprTree.Expr.Accept(this);
			EmitInsn("ldlen");
			return true;
		}

		public bool visitExpr(ArrayAccessExprTree tree)
		{
			tree.ArrayExpr.Accept(this);
			tree.IndexExpr.Accept(this);
			EmitInsn("ldelem." + tree.Type.CilTypePrefix);
			return true;
		}

		public bool visitExpr(NullLiteralTree nullLiteralTree)
		{
			EmitInsn("ldnull");
			return true;
		}

		public bool visitExpr(FieldAccessExprTree tree)
		{
			tree.Expr.Accept(this);
			EmitInsn("ld" + tree.Binding.ToString());
			return true;
		}

		public bool visitExpr(MethodCallExprTree tree)
		{
			tree.Expr.Accept(this);
			foreach (var arg in tree.Args)
				arg.Accept(this);
			EmitInsn("callvirt instance " + tree.ResolvedMethod.GetCilCallSignature());
			return true;
		}

		public bool visitExpr(NewExprTree tree)
		{
			if (tree.ArrayLengthExpr != null)
			{
				tree.ArrayLengthExpr.Accept(this);
				EmitInsn("newarr " + ((JavaArrayType)tree.Type).BaseType.CilFullClassName);
			}
			else
				EmitInsn("newobj instance void " + tree.Type.CilTypeName + "::'.ctor'()");
			return true;
		}

		public bool visitStmt(AssertStmtTree tree)
		{
			tree.Expr.Accept(this);
			EmitInsn("ldstr \"" + tree.Location.ToString() + "\"");
			EmitInsn("call void class MiniJava.RuntimeHelpers::Assert(bool, string)");
			return true;
		}

		public bool visitStmt(VariableDeclTree tree)
		{
			return true;
		}

		public bool visitStmt(WhileStmtTree tree)
		{
			string startLbl = MakeLabel();
			string condLbl = MakeLabel();

			EmitInsn("br " + condLbl);
			EmitLabel(startLbl);
			tree.Body.Accept(this);
			EmitLabel(condLbl);
			tree.Condition.Accept(this);
			EmitInsn("brtrue " + startLbl);

			return true;
		}

		public bool visitStmt(PrintStmtTree tree)
		{
			string argType;
			if (tree.Expr == null)
				argType = "";
			else if (tree.Expr.Type is JavaPrimitiveType)
				argType = tree.Expr.Type.CilTypeName;
			else
				argType = "object";

			if (tree.Expr != null)
				tree.Expr.Accept(this);
			EmitInsn("call void class [mscorlib]System.Console::WriteLine(" + argType + ")");
			return true;
		}

		public bool visitStmt(IfStmtTree tree)
		{
			String elseLabel = MakeLabel();
			tree.Condition.Accept(this);
			EmitInsn("brfalse " + elseLabel);
			tree.Then.Accept(this);

			if (tree.Else != null)
			{
				String endLabel = MakeLabel();
				EmitInsn("br " + endLabel);
				EmitLabel(elseLabel);
				tree.Else.Accept(this);
				EmitLabel(endLabel);
			}
			else
				EmitLabel(elseLabel);
			return true;
		}

		public bool visitStmt(ReturnStmtTree returnStmtTree)
		{
			if (returnStmtTree.Expr != null)
				returnStmtTree.Expr.Accept(this);
			EmitInsn("ret");
			return true;
		}

		public bool visitStmt(ExprStmtTree exprStmtTree)
		{
			exprStmtTree.Expr.Accept(this);
			if (exprStmtTree.Expr.Type != JavaType.Void)
				EmitInsn("pop");
			return true;
		}

		public bool visitStmtList(StmtListTree tree)
		{
			foreach (var stmt in tree.Statements)
				stmt.Accept(this);
			return true;
		}
	}
}
