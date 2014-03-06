using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Frontend.Trees;
using MiniJava.Frontend;

namespace MiniJava.Frontend.Trees
{
	public interface ITreeVisitor<TExprRet, TStmtRet>
	{
		TExprRet visitExpr(BinopTree tree);
		TExprRet visitExpr(LiteralValueTree tree);
		TExprRet visitExpr(UnopTree tree);
		TExprRet visitExpr(VariableTree tree);
		TExprRet visitExpr(ThisLiteralTree thisLiteralTree);
		TExprRet visitExpr(ArrayLengthExprTree arrayLengthExprTree);
		TExprRet visitExpr(ArrayAccessExprTree arrayAccessExprTree);
		TExprRet visitExpr(NullLiteralTree nullLiteralTree);
		TExprRet visitExpr(FieldAccessExprTree fieldAccessExprTree);
		TExprRet visitExpr(MethodCallExprTree methodCallExprTree);
		TExprRet visitExpr(NewExprTree newExprTree);

		TStmtRet visitStmt(AssertStmtTree tree);
		TStmtRet visitStmt(VariableDeclTree tree);
		TStmtRet visitStmt(WhileStmtTree tree);
		TStmtRet visitStmt(PrintStmtTree tree);
		TStmtRet visitStmt(IfStmtTree ifStmtTree);
		TStmtRet visitStmt(ReturnStmtTree returnStmtTree);
		TStmtRet visitStmt(ExprStmtTree exprStmtTree);

		TStmtRet visitStmtList(StmtListTree tree);
	}
}
