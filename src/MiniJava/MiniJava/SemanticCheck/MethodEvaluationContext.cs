using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Frontend;
using MiniJava.Frontend.Trees;

namespace MiniJava.SemanticCheck
{
	/// <summary>
	/// Semantic checking context for a class method.
	///
	/// Implements the following:
	/// * Lexically scoped symbol table
	/// * CLR register allocation
	/// </summary>
	public class MethodEvaluationContext
	{
		private int nextLocalRegister, nextArgumentRegister;
		private LinkedList<List<String>> ScopeStack { get; set; }
		private Dictionary<String, VariableDeclTree> LocalVariables { get; set; }
		public ClassTree Class { get; protected set; }
		public MethodTree Method { get; protected set; }
		public List<JavaType> LocalVariableRegisters { get; protected set; }

		public MethodEvaluationContext(ClassTree cls, MethodTree meth)
		{
			Class = cls;
			Method = meth;
			ScopeStack = new LinkedList<List<string>>();
			LocalVariables = new Dictionary<string,VariableDeclTree>();
			LocalVariableRegisters = new List<JavaType>();
			// in a non-static method, the this pointer is arg #0,
			// and our main method doesn't have any arguments
			nextArgumentRegister = 1;
		}
		public T WithNewScope<T>(Func<T> func)
		{
			List<String> locals = new List<string>();
			ScopeStack.AddLast(locals);
			try
			{
				return func();
			}
			finally
			{
				ScopeStack.RemoveLast();
				foreach(var variable in locals)
					LocalVariables.Remove(variable);
			}
		}
		public void DeclareLocalVariable(VariableDeclTree decl, bool isArgument)
		{
			if (isArgument)
			{
				decl.Binding = new MethodArgumentBinding(nextArgumentRegister);
				nextArgumentRegister++;
			}
			else
			{
				LocalVariableRegisters.Add(decl.Type);
				decl.Binding = new LocalVariableBinding(nextLocalRegister);
				nextLocalRegister++;
			}
			var top = ScopeStack.Last.Value;
			top.Add(decl.Var);
			LocalVariables[decl.Var] = decl;
		}
		public VariableDeclTree GetDeclaration(string name)
		{
			return LocalVariables.ContainsKey(name) ? LocalVariables[name] : null;
		}
	}
}
