namespace Giles.Console;

public class ConsoleCountsWatcher
{
    public static int ShowTop = 10;
    public static int FilenameDisplayWidth = 90;
    private class Counts
    {
        public int Created { get; set; }
        public int Updated { get; set; }
        public int Deleted { get; set; }

        public Counts(WatcherChangeTypes changeType)
        {
            IncrementChangeType(changeType);
                
        }

        public void IncrementChangeType(WatcherChangeTypes changeType)
        {
            switch (changeType)
            {
                case WatcherChangeTypes.Created:
                    Created++;
                    break;
                case WatcherChangeTypes.Deleted:
                    Deleted++;
                    break;
                case WatcherChangeTypes.Changed:
                    Updated++;
                    break;
                case WatcherChangeTypes.Renamed:
                    break;
                case WatcherChangeTypes.All:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(changeType), changeType, null);
            }
        }
    }

    private static readonly Dictionary<string, Counts> _counts = new();

    public static void Setup(FileSystemWatcher w)
    {
        w.Changed += ChangeDetected;
        w.Created += ChangeDetected;
        w.Deleted += ChangeDetected;
        w.Renamed += RenameDetected;
        w.Error += WatchError;
    }
    public static void WatchError(object sender, ErrorEventArgs e)
    {
        System.Console.WriteLine($"Error: {e.GetException()}");
    }

    public static void RenameDetected(object sender, RenamedEventArgs e)
    {
        // System.Console.WriteLine($"Rename: {e.OldName} to {e.Name}");
    }

    public static void ChangeDetected(object sender, FileSystemEventArgs e)
    {
        // NOTE: Ignore directory changes, they are picked up by the watcher.IncludeSubDirectories = true;
        if (!Directory.Exists(e.FullPath))
        {
            System.Console.WriteLine($"{e.ChangeType.ToString()}: {e.FullPath}");
        }

        if (!_counts.TryGetValue(e.FullPath, out var counts))
        {
            counts = new Counts(e.ChangeType);
            _counts.Add(e.FullPath, counts);
        }
        else
        {
            counts.IncrementChangeType(e.ChangeType);
        }

        ShowCounts();
    }

    private static void ShowCounts()
    {
        System.Console.Clear();

        var table = ShowTop > 0
            ? _counts.OrderByDescending(c => c.Value.Deleted + c.Value.Created + c.Value.Updated)
                .Take(ShowTop)
                .ToList()
            : _counts.OrderByDescending(c => c.Value.Deleted + c.Value.Created + c.Value.Updated)
                .ToList();


        string GetDisplayFilename(string filename)
        {
            if (filename.Length <= FilenameDisplayWidth)
            {
                return filename.PadRight(FilenameDisplayWidth);
            }

            var left = filename.Substring(0, (FilenameDisplayWidth / 2) - 1);
            var right = filename.Substring(filename.Length - (FilenameDisplayWidth / 2 - 2));
            return left + "..." + right;
        }

        System.Console.WriteLine($"{GetDisplayFilename("Path")} {"C",-5} {"U",-5} {"D",-5}");
        foreach (var changeCount in table)
        {
            System.Console.WriteLine(
                $"{GetDisplayFilename(changeCount.Key)} {changeCount.Value.Created,-5} {changeCount.Value.Updated,-5} {changeCount.Value.Deleted,-5} ");
        }
    }
}