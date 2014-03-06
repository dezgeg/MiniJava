using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Frontend.Trees;

namespace MiniJava.Frontend
{
	public partial class Parser
	{
		public Parser(Scanner sc, CompilerContext ctx) : base(sc)
		{
			CompilerContext = ctx;
		}
		public Parser(Scanner sc) : this(sc, new CompilerContext()) { }

		private void AddField(Location loc, VariableDeclTree decl)
		{
			if (CurrentClass.Fields.ContainsKey(decl.Var))
			{
				CompilerContext.AddError(new CompileError(
					String.Format("Duplicate field '{0}'", decl.Var), loc,
					String.Format("Field '{0}' first declared", decl.Var), CurrentClass.Fields[decl.Var].Location));
				decl.Var = CompilerContext.GetNoncollidingName(decl.Var);
			}
			CurrentClass.Fields.Add(decl.Var, new FieldTree(loc, decl.Var, decl.Type));
		}
		private void AddMethod(Location declLoc, Location loc, string name, JavaType retType, LinkedList<VariableDeclTree> args, StmtListTree body)
		{
			if (CurrentClass.Methods.ContainsKey(name))
			{
				CompilerContext.AddError(new CompileError(
					String.Format("Duplicate method '{0}'", name), declLoc,
					String.Format("Method '{0}' first declared", name), CurrentClass.Methods[name].DeclLocation));
				name = CompilerContext.GetNoncollidingName(name);
			}
			MethodTree meth = new MethodTree(declLoc, loc, name, false, retType, new List<VariableDeclTree>(args), body);
			CurrentClass.Methods.Add(name, meth);
		}
		private void AddMainMethod(Location declLoc, Location loc, StmtListTree body)
		{
			if (CompilerContext.MainClass != null)
			{
				CompilerContext.AddError(new CompileError(
					"Duplicate declaration of main method", loc,
					String.Format("Main method first declared in class '{0}'", CompilerContext.MainClass.Name),
					CompilerContext.MainClass.Methods["main"].DeclLocation));
				return;
			}
			CurrentClass.Methods.Add("main",
				new MethodTree(declLoc, loc, "main", true, JavaType.Void, new List<VariableDeclTree>(), body));
			CompilerContext.MainClass = CurrentClass;
		}
		private void BeginClass(Location declLoc, string name, string superName)
		{
			CurrentClass = new ClassTree(declLoc, name, superName);
			if (Classes.ContainsKey(name))
			{
				CompilerContext.AddError(new CompileError(
					String.Format("Duplicate class '{0}'", name), declLoc,
					String.Format("Class '{0}' first declared", name),
					Classes[name].DeclLocation));
				name = CompilerContext.GetNoncollidingName(name);
			}
			Classes.Add(name, CurrentClass);
		}
		private void EndClass(Location loc)
		{
			CurrentClass.Location = loc;
		}
	}
}
