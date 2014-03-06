using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.Frontend
{
	public abstract class Tree
	{
		public Location Location { get; protected set; }
		protected Tree(Location loc)
		{
			Location = loc;
		}
		public abstract override string ToString();
		public override bool Equals(object obj)
		{
			return ToString().Equals(obj.ToString());
		}
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
	}
}
