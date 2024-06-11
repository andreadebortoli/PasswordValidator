namespace FileHandler;

public class FileWriter : IWriter
{
    private readonly string _path;

    private readonly string _fileName;

    public FileWriter(string path, string filename)
    {
        _path = string.IsNullOrWhiteSpace(path)
            ? throw new ArgumentException("Path cannot be null or whitespace", nameof(path))
            : Path.Combine(path, filename);
        _fileName = string.IsNullOrWhiteSpace(filename)
            ? throw new ArgumentException("Filename cannot be null or whitespace", nameof(filename))
            : filename;
    }

    public void WriteToFile(string text)
    {
        try
        {
            using var writer = new StreamWriter(_path);
            writer.WriteLine(text);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            throw;
        }
    }
}