namespace FileHandler;

public class FileWriter : IWriter
{
    private readonly string _path;

    public FileWriter(string path, string filename)
    {
        var basePath = string.IsNullOrWhiteSpace(path)
            ? Directory.GetCurrentDirectory()
            : path;

        _path = Path.Combine(basePath, filename ?? throw new ArgumentException("Filename cannot be null or whitespace", nameof(filename)));
    }

    public void WriteToFile(string text)
    {
        try
        {
            using var writer = new StreamWriter(_path, append:true);
            writer.WriteLine(text);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            throw;
        }
    }
}