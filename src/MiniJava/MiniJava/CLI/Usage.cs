using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniJava.CLI
{
	public static class Usage
	{
		public static void ShowUsage()
		{
			Console.Error.WriteLine(@"usage: 'MiniJava [<args>] <filename>' or 'MiniJava [<args>] (-e|-s) <code>'

The first version loads the code to be executed from the specified file.
For the second version, the code is supplied on the command line as follows:
    -e    evaluate <code> as expression, and print the result
          (i.e. wrap <code> in a main class, a main method and a println()
    -s    evaluate <code> as statements
          (i.e. wrap <code> in a main class and a main method)
    Remember to escape your code according to your shell's escaping rules.

When compiling a file, the resulting binary will be written to a file with the
same basename as the source file, with a .exe extension, unless the -t flag is given.

Arguments:
    -d    dump the generated CIL code to stderr
    -t    instead of generating an .exe, compile to an temporary file and run it directly
");
		}
	}
}
