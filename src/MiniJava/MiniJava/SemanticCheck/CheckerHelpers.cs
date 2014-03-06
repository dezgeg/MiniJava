using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Frontend.Trees;
using MiniJava.Frontend;

namespace MiniJava.SemanticCheck
{
	public partial class Checker : ITreeVisitor<JavaType, bool>
	{
		/// <summary>
		/// Typecheck the expression, and bind the type to the tree.
		/// Always call this instead of directly calling expr.Accept(this)!
		/// </summary>
		private JavaType evalExpr(ExprTree expr)
		{
			// This might return a null value, but it doesn't matter since
			// the code generator is not run for erroneous programs.
			var exprType = expr.Accept(this);
			expr.Type = exprType;
			return exprType;
		}
		private bool checkTypeValidity(JavaType type, string description, Location loc,
			bool allowPrimitive = true, bool allowVoid = false, bool allowArray = true)
		{
			String error = null;
			if (type is NullConstantType)
				error = "Null literal type is not acceptable";
			else if (type is JavaPrimitiveType)
			{
				if (!allowPrimitive)
					error = String.Format("Primitive type '{0}' is not acceptable", type);
				else if (!allowVoid && type == JavaType.Void)
					error = "Type 'void' is not acceptable";
			}
			else if (type is JavaClassType)
			{
				if(!Context.Classes.ContainsKey(((JavaClassType)type).Name))
					error = String.Format("Nonexisting class '{0}'", type);
			}
			else if (type is JavaArrayType)
			{
					var baseType = (((JavaArrayType)type).BaseType);
					if (baseType == JavaType.Void)
						error = "Array of type 'void' is not acceptable";
					else if (error == null && !allowArray)
						error = String.Format("Array type {0} is not acceptable", type);
					else
						return checkTypeValidity(baseType, description, loc, true, false);
			}
			if (error != null)
			{
				Context.AddError(new CompileError(error + " in " + description, loc));
				return false;
			}
			return true;
		}
		private JavaType checkExprHasType(ExprTree exprTree, JavaType expected, string exprDescription)
		{
			JavaType actual = evalExpr(exprTree);
			if (actual != null && !expected.Equals(actual))
			{
				var e = new CompileError(String.Format("{0} should have type '{1}', but has '{2}'",
					exprDescription, expected, actual), exprTree.Location);
				Context.AddError(e);
			}
			return expected;
		}
		private JavaType checkExprHasArrayType(ExprTree exprTree)
		{
			JavaType typ = evalExpr(exprTree);
			if (typ == null)
				return null;
			if (!(typ is JavaArrayType))
			{
				Context.AddError(new CompileError(
					String.Format("Array type expected instead of '{0}'", typ),
					exprTree.Location));
				return null;
			}
			return ((JavaArrayType)typ).BaseType;
		}
		private ClassTree lookupClass(JavaType typ)
		{
			return Context.Classes[((JavaClassType)typ).Name];
		}
		private Tuple<MethodTree, ClassTree> lookupMethod(ClassTree startClass, String methodName)
		{
			foreach (var cls in startClass.EnumSelfAndAncestors())
			{
				if (!cls.Methods.ContainsKey(methodName))
					continue;
				return new Tuple<MethodTree, ClassTree>(cls.Methods[methodName], cls);
			}
			return null;
		}
		/// <summary>
		/// Look up a field by name in a class and its ancestors.
		/// Returns the FieldTree, the ClassTree the field was found, and whether the access is allowed, or null on failure.
		/// </summary>
		private Tuple<FieldTree, ClassTree, bool> resolveField(ClassTree startClass, String fieldName, Location loc, bool allowMain)
		{
			bool isOk = true;
			foreach (var cls in startClass.EnumSelfAndAncestors())
			{
				if (!cls.Fields.ContainsKey(fieldName))
					continue;
				FieldTree field = cls.Fields[fieldName];
				if (MethodContext.Method.IsMain && !allowMain)
				{
					Context.AddError(new CompileError(
						String.Format("Cannot reference non-static member '{0}' in static context", fieldName),
						loc));
					isOk = false;
				}
				if (MethodContext.Class.Name != cls.Name)
				{
					Context.AddError(new CompileError(
						String.Format("Cannot access field '{0}' private to class '{1}' from class '{2}'", fieldName, cls.Name, MethodContext.Class.Name),
						loc));
					isOk = false;
				}
				return new Tuple<FieldTree, ClassTree, bool>(field, cls, isOk);
			}
			return null;
		}
		private bool isAssignableTo(JavaType rhs, JavaType lhs)
		{
			if (rhs is NullConstantType)
				return (lhs is JavaClassType || lhs is JavaArrayType || lhs is NullConstantType);
			else if (lhs is NullConstantType)
				return false;
			else if (lhs is JavaPrimitiveType || rhs is JavaPrimitiveType)
				return lhs.Equals(rhs);
			else if (lhs is JavaArrayType && rhs is JavaArrayType)
				return isAssignableTo(((JavaArrayType)rhs).BaseType, ((JavaArrayType)lhs).BaseType);
			else if (rhs is JavaClassType && lhs is JavaClassType)
			{
				foreach (var rhsAncestor in lookupClass(rhs).EnumSelfAndAncestors())
					if (lookupClass(lhs) == rhsAncestor)
						return true;
			}
			return false;
		}
		private JavaType checkExprsHaveTypes(ExprTree expr1, ExprTree expr2, JavaType expected, string opName, Location location)
		{
			JavaType typ1 = evalExpr(expr1), typ2 = evalExpr(expr2);
			if(typ1 == null || typ2 == null)
				return expected;
			if (!typ1.Equals(expected) || !typ2.Equals(expected))
			{
				Context.AddError(new CompileError(
				String.Format("Both operands to {0} should have type '{1}', but are '{2}' and '{3}'", opName, expected, typ1, typ2),
				location
				));
			}
			return expected;
		}
		private void declareVariable(VariableDeclTree decl, bool isArgument)
		{
			VariableDeclTree oldDecl = null;
			if((oldDecl = MethodContext.GetDeclaration(decl.Var)) != null)
			{
				Context.AddError(new CompileError(
					String.Format("Local variable '{0}' already declared", decl.Var),
					decl.Location,
					String.Format("'{0}' previously declared", decl.Var),
					oldDecl.Location));
				return;
			}
			if (!checkTypeValidity(decl.Type, (isArgument ? "method argument" : "variable") + " declaration", decl.Location))
				return;

			MethodContext.DeclareLocalVariable(decl, isArgument);
		}
		private JavaType bindVariable(VariableTree varTree)
		{
			VariableDeclTree decl;
			if ((decl = MethodContext.GetDeclaration(varTree.Name)) != null)
			{
				varTree.Binding = decl.Binding;
				return decl.Type;
			}
			Tuple<FieldTree, ClassTree, bool> result = resolveField(MethodContext.Class, varTree.Name, varTree.Location, false);
			if(result != null)
			{
				if (result.Item3)
				{
					varTree.Binding = new FieldBinding(result.Item2, result.Item1);
					return result.Item1.Type;
				}
				return null;
			}
			Context.AddError(new CompileError(
				String.Format("Unknown symbol '{0}'", varTree.Name),
				varTree.Location));
			return null;
		}
	}
}
