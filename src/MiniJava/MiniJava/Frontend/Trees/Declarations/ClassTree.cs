using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Frontend.Trees
{
	public class ClassTree
	{
		public ClassTree(Location declLoc, string name, string superName)
		{
			Name = name;
			SuperClassName = superName;
			Fields = new Dictionary<string, FieldTree>();
			Methods = new Dictionary<string, MethodTree>();
			DeclLocation = declLoc;
		}
		public Location DeclLocation { get; set; }
		public Location Location { get; set; }
		public String Name { get; set; }
		public Dictionary<String, FieldTree> Fields { get; private set; }
		public Dictionary<String, MethodTree> Methods { get; private set; }
		public String SuperClassName { get; set; }
		// This will be filled during the beginning of semantic check, not during parsing!
		public ClassTree SuperClass { get; set; }

		/// <summary>
		/// Enumerate all the superclasses of this class.
		/// </summary>
		public IEnumerable<ClassTree> EnumAncestors()
		{
			ClassTree cls = SuperClass;
			while (cls != null)
			{
				yield return cls;
				cls = cls.SuperClass;
			}
		}
		/// <summary>
		/// Enumerate this class, and all its superclasses
		/// </summary>
		public IEnumerable<ClassTree> EnumSelfAndAncestors()
		{
			yield return this;
			foreach (var ancestor in EnumAncestors())
				yield return ancestor;
		}
	}
}
