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
		public CompilerContext Context { get; protected set; }
		public MethodEvaluationContext MethodContext { get; protected set; }
		public Checker(CompilerContext ctx)
		{
			Context = ctx;
		}
		public void Check()
		{
			// Yes, order matters.
			foreach (var cls in Context.Classes.Values)
				resolveSuperClasses(cls);
			foreach (var cls in Context.Classes.Values)
				checkInheritanceLoop(cls);
			foreach (var cls in Context.Classes.Values)
				foreach (var fld in cls.Fields.Values)
					checkField(cls, fld);
			foreach (var cls in Context.Classes.Values)
				foreach (var meth in cls.Methods.Values)
					checkMethod(cls, meth);
		}
		/// <summary>
		/// Check for superclass existence, and fill the SuperClass field of cls
		/// </summary>
		private void resolveSuperClasses(ClassTree cls)
		{
			if (cls.SuperClassName == null)
				return;
			if (!Context.Classes.ContainsKey(cls.SuperClassName))
			{
				Context.AddError(new CompileError(
					String.Format("Class '{0}' has a nonexisting superclass '{1}'", cls.Name, cls.SuperClassName),
					cls.DeclLocation));
				cls.SuperClassName = null;
			}
			else
				cls.SuperClass = Context.Classes[cls.SuperClassName];
		}
		private void checkInheritanceLoop(ClassTree cls)
		{
			List<string> seen = new List<string>();
			while (cls != null && cls.SuperClassName != null)
			{
				bool loop = seen.Contains(cls.Name);
				seen.Add(cls.Name);
				if (loop)
				{
					Context.AddError(new CompileError("Inheritance loop detected: " +
						String.Join(" -> ", seen), cls.DeclLocation));
					cls.SuperClass = null;
					cls.SuperClassName = null;
				}
				cls = cls.SuperClass;
			}
		}
		private void checkField(ClassTree cls, FieldTree fld)
		{
			checkTypeValidity(fld.Type,
				String.Format("field '{0}' of class '{1}'", fld.Name, cls.Name),
				fld.Location);
		}

		private void checkMethod(ClassTree cls, MethodTree meth)
		{
			foreach (var ancestor in cls.EnumAncestors().Reverse())
			{
				if (ancestor.Methods.ContainsKey(meth.Name))
				{
					meth.EarliestDeclaringClass = ancestor;
					var ancestorMethod = ancestor.Methods[meth.Name];
					if (!ancestorMethod.Equals(meth))
						Context.AddError(new CompileError(
							String.Format("Cannot declare method '{0}' since its type is different from the superclasses' method", meth),
							meth.DeclLocation,
							String.Format("Superclass method '{0}' declared", ancestorMethod),
							ancestorMethod.DeclLocation
					));
					break;
				}
			}
			if (meth.EarliestDeclaringClass == null)
				meth.EarliestDeclaringClass = cls;

			checkTypeValidity(meth.ReturnType,
				String.Format("return type of method '{0}' in class '{1}'", meth, cls.Name),
				meth.DeclLocation,
				allowVoid: true);
			MethodContext = new MethodEvaluationContext(cls, meth);
			MethodContext.WithNewScope(() =>
			{
				foreach (var arg in meth.Arguments)
					declareVariable(arg, true);

				if (!meth.Body.Accept(this) && !meth.ReturnType.Equals(JavaType.Void))
					Context.AddError(new CompileError("Missing return statement",
						meth.Body.Statements.Count == 0 ? meth.Location : meth.Body.Statements.Last().Location));
				meth.LocalVariableRegisters = MethodContext.LocalVariableRegisters;
				return true; // Dummy
			});
		}
	}
}
