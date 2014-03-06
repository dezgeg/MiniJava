using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Frontend.Trees
{
	public class FieldTree
	{
		public FieldTree(Location loc, String name, JavaType type)
		{
			Name = name;
			Type = type;
			Location = loc;
		}

		public Location Location { get; protected set; }
		public String Name { get; private set; }
		public JavaType Type { get; private set; }
	}
}
