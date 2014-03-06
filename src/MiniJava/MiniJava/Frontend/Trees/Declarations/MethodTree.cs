using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Frontend.Trees
{
	public class MethodTree
	{
		public MethodTree(Location declLoc, Location loc, string name, bool isMain, JavaType retType, List<VariableDeclTree> args,
				StmtListTree body)
		{
			DeclLocation = declLoc;
			Location = loc;
			Name = name;
			IsMain = isMain;
			ReturnType = retType;
			Arguments = args;
			Body = body;
		}
		public Location DeclLocation { get; protected set; }
		public Location Location { get; protected set; }
		public string Name { get; private set; }
		public JavaType ReturnType { get; protected set; }
		public List<VariableDeclTree> Arguments { get; protected set; }
		public StmtListTree Body { get; protected set; }
		public bool IsMain { get; protected set; }
		/// <summary>
		/// List of local variable register types. Filled by SemanticCheck in MethodEvaluationContext 
		/// </summary>
		public List<JavaType> LocalVariableRegisters { get; set; }
		/// <summary>
		/// Class where the method is first defined in the inheritance hierarcy.
		/// e.g for this: class Foo { void meth() {} }, class Bar extends Foo { void meth() {} },
		/// the earliest declaring class is Foo.
		/// Even for virtual calls, this is required when generating CIL
		/// </summary>
		public ClassTree EarliestDeclaringClass { get; set; }
		public override string ToString()
		{
			return String.Format("public {0}{1} {2}({3})",
				IsMain ? "static " : "",
				ReturnType, Name,
				String.Join(", ", Arguments));
		}
		/// <summary>
		/// Returns a CIL signature used in declations for this method. For example, "class Foo getFoo(int32 index, class Foo[] foos)"
		/// TODO: This CIL-dependent stuff should really be moved somewhere else
		/// </summary>
		public string GetCilSignature()
		{
			return String.Format("{0} {1}({2})",
				ReturnType.CilTypeName, Name,
				String.Join(", ", Arguments.Select(arg => arg.Type.CilTypeName)));
		}
		/// <summary>
		/// Returns a CIL signature used to call this method. For example, in "callvirt instance class ReturnClass ClassName::getBar(int32 x)"
		///                                                            this part:                ^------------------------------------------^
		/// </summary>
		public string GetCilCallSignature()
		{
			return String.Format("{0} class {1}::{2}({3})",
				ReturnType.CilTypeName, EarliestDeclaringClass.Name, Name,
				String.Join(", ", Arguments.Select(arg => arg.Type.CilTypeName)));
		}
		public override bool Equals(object obj)
		{
			return ToString() == obj.ToString();
		}
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
	}
}
