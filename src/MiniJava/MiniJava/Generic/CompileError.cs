using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Frontend;

namespace MiniJava
{
	public class CompileError : IComparable<CompileError>
	{
		public string Message { get; set; }
		public Location Location { get; set; }
		public String ContextMessage { get; protected set; }
		public Location ContextLocation { get; protected set; }

		public CompileError(string message, Location loc=new Location())
		{
			Message = message;
			Location = loc;
		}
		public CompileError(string message, Location loc, String contextMessage, Location contextLoc)
		{
			Message = message;
			Location = loc;
			ContextMessage = contextMessage;
			ContextLocation = contextLoc;
		}
		public int CompareTo(CompileError other)
		{
			return Location.CompareTo(other.Location);
		}
	}
}
