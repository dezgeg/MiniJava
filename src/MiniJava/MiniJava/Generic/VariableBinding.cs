using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Frontend.Trees;

namespace MiniJava
{
	/// <summary>
	/// Represents a binding of a variable.
	/// For example, "foo" could refer to a local variable, method argument, or a field in the current class.
	/// </summary>
	public abstract class VariableBinding
	{
	}
	public abstract class RegisterBinding : VariableBinding
	{
		public int Register { get; protected set; }
		public RegisterBinding(int r)
		{
			Register = r;
		}
		/// <summary>
		/// The ToString method returns the suffix of the CIL instruction used to access this value.
		/// For example, local variables can be accessed with ldloc 1 and stloc 1, so this method would return "loc 1" for such variable.
		/// </summary>
		public override string ToString()
		{
			throw new NotImplementedException("This must be implemented");
		}
	}
	public class LocalVariableBinding : RegisterBinding
	{
		public LocalVariableBinding(int r) : base(r) { }
		public override string ToString()
		{
			return "loc " + Register;
		}
	}
	public class MethodArgumentBinding : RegisterBinding
	{
		public MethodArgumentBinding(int r) : base(r) { }
		public override string ToString()
		{
			return "arg " + Register;
		}
	}
	public class FieldBinding : VariableBinding
	{
		public ClassTree Class { get; protected set; }
		public FieldTree Field { get; protected set; }

		public FieldBinding(ClassTree classTree, FieldTree fld)
		{
			Class = classTree;
			Field = fld;
		}
		public override string ToString()
		{
			return String.Format("fld {0} {1}::{2}", Field.Type.CilTypeName, Class.Name, Field.Name);
		}
	}
}
