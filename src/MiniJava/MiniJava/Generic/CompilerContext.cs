using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Frontend.Trees;

namespace MiniJava
{
	public class CompilerContext
	{
		private int nameGenCounter;
		public List<CompileError> Errors { get; private set; }
		public IDictionary<string, ClassTree> Classes { get; private set; }
		public ClassTree MainClass { get; set; }

		public CompilerContext()
		{
			Errors = new List<CompileError>();
			Classes = new Dictionary<String, ClassTree>();
		}
		public void AddError(CompileError ce)
		{
			Errors.Add(ce);
		}
		public String GetNoncollidingName(string name)
		{
			return name + "#" + ++nameGenCounter;
		}
	}
}