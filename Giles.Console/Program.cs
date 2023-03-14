using System.IO.Enumeration;
using System.Reflection;

namespace Giles.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowHeader();

            if (args.Length == 0)
            {
                ShowError("No path specified");
                ShowUsage();
                Environment.Exit(1);
            }

            if (!Directory.Exists(args[0]))
            {
                ShowError($"Path not found: {args[0]}");
                ShowUsage();
                Environment.Exit(2);
            }

            var path = args[0];
            var w = new FileSystemWatcher(path);
            w.IncludeSubdirectories = true;

            ConsoleWatcher.Setup(w);
            // ConsoleCountsWatcher.Setup(w);

            w.EnableRaisingEvents = true;
            
            System.Console.WriteLine($"Press any key to stop watching {Path.GetFullPath(path)}");
            System.Console.ReadKey();
        }



        private static void ShowError(string msg)
        {
            System.Console.WriteLine(msg);
        }

        private static void ShowUsage()
        {
            System.Console.WriteLine("GILES path");

        }

        private static void ShowHeader()
        {
            var asm = Assembly.GetExecutingAssembly();
            var d = new FileInfo(asm.Location);
            System.Console.WriteLine($"Giles v{asm.GetName().Version} of {d.CreationTime}");
        }
    }
}