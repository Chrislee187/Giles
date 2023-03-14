using System.Diagnostics;
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
                ShowOptionsError("No path specified", (int)OptionsErrors.PathNotSpecified);
            }

            if (!Directory.Exists(args[0]))
            {
                ShowOptionsError($"Path not found: {args[0]}", (int)OptionsErrors.PathNotFound);
            }

            var path = args[0];

            var w = new FileSystemWatcher(path)
            {
                IncludeSubdirectories = true
            };

            var watchMode = GetWatchModeOption(args);

            SetupWatcher(watchMode, w);

            w.EnableRaisingEvents = true;

            System.Console.WriteLine($"Press any key to stop watching {Path.GetFullPath(path)}");
            System.Console.ReadKey();
        }

        private static void SetupWatcher(WatchMode watchMode, FileSystemWatcher w)
        {
            switch (watchMode)
            {
                case WatchMode.Default:
                    ConsoleWatcher.Setup(w);
                    break;
                case WatchMode.Counts:
                    ConsoleCountsWatcher.Setup(w);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static WatchMode GetWatchModeOption(string[] args)
        {
            var watchMode = WatchMode.Default;

            if (args.Length < 2) return watchMode;

            switch (args[1].ToLower())
            {
                case "count":
                case "counts":
                    watchMode = WatchMode.Counts;
                    break;
                default:
                    ShowOptionsError($"Invalid mode: {args[1]}", (int)OptionsErrors.InvalidWatchMode);
                    break;
            }

            return watchMode;
        }
        
        private static void ShowOptionsError(string msg, int exitCode = -1)
        {
            System.Console.WriteLine(msg);
            ShowUsage();
            Environment.Exit(exitCode);
        }

        private static void ShowHeader()
        {
            var asm = Assembly.GetExecutingAssembly();
            var d = new FileInfo(asm.Location);
            System.Console.WriteLine($"Giles v{asm.GetName().Version} of {d.CreationTime}");
        }

        private static void ShowUsage() => System.Console.WriteLine("GILES path-to-watch [count[s]]");
    }

    public enum OptionsErrors
    {
        PathNotSpecified = -1,
        PathNotFound = -2,
        InvalidWatchMode = -3

    }

    public enum WatchMode
    {
        Default, Counts
    }
}