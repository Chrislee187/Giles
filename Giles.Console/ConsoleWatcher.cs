namespace Giles.Console;

public static class ConsoleWatcher
{

    public static void Setup(FileSystemWatcher w)
    {
        w.Changed += ChangeDetected;
        w.Created += CreateDetected;
        w.Deleted += DeleteDetected;
        w.Renamed += RenameDetected;
        w.Error += WatchError;
    }
    public static void WatchError(object sender, ErrorEventArgs e)
    {
        System.Console.WriteLine($"Error: {e.GetException()}");
    }

    public static void RenameDetected(object sender, RenamedEventArgs e)
    {
        System.Console.WriteLine($"Rename: {e.OldName} to {e.Name}");
    }

    public static void DeleteDetected(object sender, FileSystemEventArgs e)
    {
        System.Console.WriteLine($"{e.ChangeType.ToString()}: {e.FullPath}");
    }

    public static void CreateDetected(object sender, FileSystemEventArgs e)
    {
        System.Console.WriteLine($"{e.ChangeType.ToString()}: {e.FullPath}");
    }

    public static void ChangeDetected(object sender, FileSystemEventArgs e)
    {
        // NOTE: Ignore directory changes, they are picked up by the watcher.IncludeSubDirectories = true;
        if (!Directory.Exists(e.FullPath))
        {
            System.Console.WriteLine($"{e.ChangeType.ToString()}: {e.FullPath}");
        }
    }
}