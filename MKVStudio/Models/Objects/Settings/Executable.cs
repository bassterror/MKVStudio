namespace MKVStudio.Models;

public class Executable
{
    public ExecutableNames Name { get; set; }
    public string Path { get; set; }

    public Executable(ExecutableNames name, string path)
    {
        Name = name;
        Path = path;
    }
}
