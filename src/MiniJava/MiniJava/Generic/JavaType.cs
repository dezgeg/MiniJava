using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava
{
	public abstract class JavaType
	{
		public static readonly JavaType Int = new JavaPrimitiveType("int", "i4", "int32", "[mscorlib]System.Int32");
		public static readonly JavaType Bool = new JavaPrimitiveType("boolean", "i1", "bool", "[mscorlib]System.Boolean");
		public static readonly JavaType Void = new JavaPrimitiveType("void", "SHOULD_NOT_HAPPEN", "void", "KILL_THIS_COMPILE");
		public static readonly JavaType NullConstantType = new NullConstantType();

		// e.g. the 'ref' in stelem.ref
		public virtual string CilTypePrefix { get; set; }
		// full classname, e.g [mscorlib]System.Int32
		public virtual string CilFullClassName { get; protected set; }
		// type name, eg. int32 or class Foo
		public virtual string CilTypeName { get; protected set; }
		public override bool Equals(object obj)
		{
			return ToString().Equals(obj.ToString());
		}
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
	}
	public class NullConstantType : JavaType
	{
		public override string ToString()
		{
			return "(null constant type)";
		}
	}
	public class JavaClassType : JavaType
	{
		public String Name { get; protected set; }
		public JavaClassType(String name)
		{
			this.Name = name;
		}
		public override string ToString()
		{
			return Name;
		}
		public override string CilTypePrefix
		{
			get { return "ref"; }
			set { throw new InvalidOperationException(); }
		}
		public override string CilFullClassName
		{
			get { return Name; }
		}
		public override string CilTypeName
		{
			get { return "class " + Name; }
		}
	}
	public class JavaPrimitiveType : JavaType
	{
		public String Name { get; protected set; }
		public JavaPrimitiveType(String name, string prefix, string typeName, string fullClassname)
		{
			Name = name;
			CilTypePrefix = prefix;
			CilTypeName = typeName;
			CilFullClassName = fullClassname;
		}
		public override string ToString()
		{
			return Name;
		}
	}
	public class JavaArrayType : JavaType
	{
		public JavaType BaseType { get; protected set; }
		public JavaArrayType(JavaType baseType)
		{
			BaseType = baseType;
		}
		public override string ToString()
		{
			return BaseType.ToString() + "[]";
		}
		public override string CilTypePrefix
		{
			// We should never have to refer anything with this (?)
			get { throw new InvalidOperationException(); }
			set { throw new InvalidOperationException(); }
		}
		public override string CilFullClassName
		{
			// We should never have to refer anything with this (?)
			get { throw new InvalidOperationException(); }
			protected set { throw new InvalidOperationException(); }
		}
		public override string CilTypeName
		{
			get { return BaseType.CilTypeName + "[]"; }
			protected set { throw new InvalidOperationException(); }
		}
	}
}
