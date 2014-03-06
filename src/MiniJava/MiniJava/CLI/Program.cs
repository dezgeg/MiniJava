using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MiniJava.Frontend;
using MiniJava.SemanticCheck;
using MiniJava.Backend;
using System.Diagnostics;

namespace MiniJava.CLI
{
	public partial class Program
	{
		public static readonly String TAB = "    ";
		// Added as fields so they get into the class diagram
		private CompilerContext Context;
		private Scanner Scanner;
		private Parser Parser;
		private Checker Checker;
		private CodeGen CodeGen;
		private string Input;
		private string FileName;
		public static Program Instance = new Program();
		public static void Main(String[] args)
		{
			Environment.ExitCode = Instance.RealMain(args);
		}
		// Entry point used by driver tests
		// Returns an exit code passed to the operating system:
		// 0 - ok
		// 1 - compile error
		// 2 - error opening source file
		// 3 - invalid command line
		// 4 - error running ilasm
		// 5 - internal error
		public int RealMain(String[] args)
		{
			bool run = false;
			bool dumpCil = false;
			Context = new CompilerContext();
			Scanner = new Scanner(Context);
			int argIndex = 0;
			// parse the -t and -d flags
			for (; argIndex < args.Length; argIndex++)
			{
				if (args[argIndex] == "-t")
					run = true;
				else if (args[argIndex] == "-d")
					dumpCil = true;
				else break;
			}
			// if compiling from command line
			if (args.Length - argIndex >= 2 && (args[argIndex] == "-e" || args[argIndex] == "-s"))
			{
				FileName = "<command line>";
				Input = String.Format((args[argIndex] == "-e")
					? "class Main {{ public static void main() {{ System.out.println(\n{0}\n);\n}}\n}}"
					: "class Main {{ public static void main() {{\n{0}\n}}\n}}", args[argIndex + 1]);
				run = true;
			}
			// if compiling from a file
			else if (args.Length - argIndex == 1 && !args[argIndex].StartsWith("-"))
				try
				{
					Input = File.ReadAllText(args[argIndex]);
					FileName = Path.GetFileName(args[argIndex]);
				}
				catch (IOException ioe)
				{
					Console.Error.WriteLine(ioe.Message);
					return 2;
				}
			else
			{
				Usage.ShowUsage();
				return 3;
			}
			return Compile(run, dumpCil);
		}

		private int Compile(bool run, bool dumpCil)
		{
			try
			{
				Input = Input.Replace("\t", TAB);
				Scanner.SetSource(Input, 0);
				Parser = new Parser(Scanner, Context);
				Parser.Parse();
				Checker = new Checker(Context);
				Checker.Check();
			}
			catch (Exception e)
			{
				Console.Error.WriteLine("----BEGIN FATAL INTERNAL ERROR----");
				Console.Error.WriteLine(e);
				Console.Error.WriteLine("----END FATAL INTERNAL ERROR----");
				return 5;
			}
			if (Context.Errors.Count != 0)
			{
				OutputErrors();
				return 1;
			}
			else
			{
				string cilFile = Path.GetTempFileName();
				TextWriter cilOutput = File.CreateText(cilFile);
				CodeGen = new CodeGen(Context, cilOutput);
				CodeGen.Generate();
				cilOutput.Close();
				string exeFile = Path.ChangeExtension(run ? Path.GetRandomFileName() : FileName, "exe");
				if (ProcessUtils.Assemble(cilFile, exeFile))
				{
					if (run)
					{
						Process proc = ProcessUtils.Exec(exeFile);
						// for some insane and stupid reason, this is REQUIRED for the unit tests to output anything ?!?
						Console.Write(proc.StandardOutput.ReadToEnd());
						Console.Error.Write(proc.StandardError.ReadToEnd());

						File.Delete(exeFile);
					}
				}
				else
				{
					Console.Error.Write(File.ReadAllText(cilFile));
					return 4;
				}
				if (dumpCil)
					Console.Error.Write(File.ReadAllText(cilFile));
				return 0;
			}
		}
	}
}
