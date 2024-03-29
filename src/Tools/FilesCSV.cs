using System.Diagnostics;
using System.IO.Compression;

namespace IDN.Tools;

public static class FilesCsv
{
    public static readonly string _DIRECTORY = @"/home/dbn/sources/s-indicadores/files/";
    public static async Task NormalizeAsync()
    => await Task.Run(async () =>
    {
        var _timer = new Stopwatch();
        _timer.Start();
        await DeleteNotZip(_DIRECTORY);

        var _listfiles = new List<string>();

        var _cores = Cpu.Count - 2;

        foreach (string file in Directory.GetFiles(_DIRECTORY).OrderBy(s => s))
            if (file.Contains(".zip") == true)
            {
                _listfiles.Add(file);
                Console.WriteLine(file);
            }

        var _tasks = _listfiles.Select(file => Task.Run(async () => await Unzip(file, _DIRECTORY)));

        await Parallel.ForEachAsync(_tasks,
            async (t, _) =>
                await t
            ); 

        _timer.Stop();
        Console.WriteLine($"Normalized Files! {_timer.Elapsed:hh\\:mm\\:ss}");
    });

    public static async Task<string[]> FilesListAsync(string path, string extension)
    => await Task.Run(() => Directory.GetFiles(path).Where(s => s.Contains(extension)).ToArray());

    private static async Task DeleteNotZip(string sourceFilePath)
    => await Task.Run(() =>
    {
        foreach (string arquivo in Directory.GetFiles(sourceFilePath))
            if (Path.GetExtension(arquivo) != ".zip")
                File.Delete(arquivo);
    });

    private static async Task Unzip(string sourceFilePath, string destinationFolderPath)
    => await Task.Run(() =>
    {
        string filename = Path.GetFileNameWithoutExtension(sourceFilePath);
        string fileextension = Path.GetExtension(sourceFilePath);

        using ZipArchive archive = ZipFile.OpenRead(sourceFilePath);
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                string filePath = Path.Combine(destinationFolderPath, entry.FullName);
                entry.ExtractToFile(filePath, true);

                File.Move(filePath, Path.Combine(Path.GetDirectoryName(filePath)!, $"{filename}{Path.GetExtension(filePath)}"));
                Console.WriteLine($"File: {filename}{Path.GetExtension(filePath)} OK");
            }
        }

    });
}