using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MiniJava.Backend;
using System.Diagnostics;

namespace MiniJava.CLI
{
	public static class ProcessUtils
	{
		public static bool Assemble(string cilFile, string exeFile)
		{
			Process ilasm = RunIlasm(cilFile, exeFile);
			if (ilasm.ExitCode != 0)
			{
				Console.Error.WriteLine("---- Ilasm exited with error code {0} ----", ilasm.ExitCode);
				Console.Error.Write(ilasm.StandardError.ReadToEnd());
				Console.Error.WriteLine("---- End of ilasm error message ----");
				return false;
			}
			return true;
		}
		public static Process RunIlasm(string cilFile, string exeFile)
		{
			string ilasmPath = Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "ilasm.exe");
			return Exec(ilasmPath, String.Format("/optimize /quiet \"{0}\" \"/output={1}\"", cilFile, exeFile));
		}
		public static Process Exec(string path, string args = "")
		{
			ProcessStartInfo info = new ProcessStartInfo(path, args);
			info.UseShellExecute = false;
			info.CreateNoWindow = true;
			info.RedirectStandardError = info.RedirectStandardOutput = true;
			Process proc = Process.Start(info);
			proc.WaitForExit();
			return proc;
		}
	}
}
