namespace MKVStudio.Models;

public class Executable
{
    public ExecutableNames Name { get; private set; }
    public string Path { get; private set; }

    public Executable(ExecutableNames name, string path)
    {
        Name = name;
        Path = path;
    }
}
