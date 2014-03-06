using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Frontend;

namespace MiniJava.CLI
{
	public partial class Program
	{
		private void OutputErrors()
		{
			// Array#Sort is not stable
			var errors = Context.Errors.OrderBy(err => err);
			foreach (var ce in errors)
				FormatError(ce);

			Console.Error.WriteLine();
			Console.Error.WriteLine(Context.Errors.Count + " errors in total.");
		}
		public void FormatError(CompileError ce)
		{
			OutputSummaryLine(ce.Location, "error", ce.Message, ConsoleColor.Red);
			FormatLocation(ce.Location);
			if (ce.ContextMessage != null)
			{
				OutputSummaryLine(ce.ContextLocation, "note", ce.ContextMessage + " here", ConsoleColor.DarkGray);
				FormatLocation(ce.ContextLocation);
			}
		}
		/// <summary>
		/// Outputs a clang-style error summary line, like "foo.c:1-2:3: prefix: msg",
		/// where the prefix has the given color
		/// </summary>
		private void OutputSummaryLine(Location loc, string prefix, string msg, ConsoleColor color)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.Error.Write("{0}:{1}: ", FileName, loc);
			Console.ForegroundColor = color;
			Console.Error.Write("{0}: ", prefix);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Error.WriteLine(msg);
			Console.ForegroundColor = ConsoleColor.Gray;
		}
		/// <summary>
		/// Outputs a clang-style context line, with colors. Such as:
		///     int x = asdasads;
		///             ^^^^^^^^
		/// </summary>
		public void FormatLocation(Location loc)
		{
			if (loc.StartLine != loc.EndLine || loc.EndLine <= 0 || loc.StartIndex < 0 || loc.EndColumn < 0)
				return;
			int start = loc.StartIndex, end = loc.EndIndex;

			// locate the beginning and end of the line containing loc
			while (start >= 0 && Input[start] != '\n')
				start--;
			while (end < Input.Length - 1 && Input[end] != '\n')
				end++;
			while (Char.IsControl(Input[end - 1]))
				end--;

			Console.Error.Write(TAB); // an extra indent makes it look a bit nicer
			for (int i = start + 1; i < end; i++)
				Console.Error.Write(SanitizeChar(Input[i]));
			Console.Error.WriteLine();

			StringBuilder contextLine = new StringBuilder();
			contextLine.Append(' ', loc.StartColumn + 4);
			contextLine.Append('^', loc.EndColumn - loc.StartColumn);

			Console.ForegroundColor = ConsoleColor.Green;
			Console.Error.WriteLine(contextLine);
			Console.ForegroundColor = ConsoleColor.Gray;
		}
		public char SanitizeChar(char c)
		{
			if (c >= '\t' && c <= '\f') // normalize the ascii whitespaces to prevent odd terminal displays
				return ' ';
			else if (c < ' ' || c >= (char)0x7f) // ascii control chars and non-ascii chars
				return '?';
			else
				return c;
		}
	}
}
