using System.IO;
using System.Linq;

namespace MKVStudio.ViewModels;

public class SourceFileVM : BaseViewModel
{
    public InputVM Input { get; set; }
    public bool IsPrimary { get; set; }
    public string InputPath { get; set; }
    public string InputName { get; set; }
    public string InputExtension { get; set; }
    public string InputFullName => InputName + InputExtension;
    public string InputFullPath { get; set; }
    public string OutputPath { get; set; }
    public string OutputName { get; set; }
    public static string OutputExtension => ".mkv";
    public string OutputFullName => $"{OutputName}.{OutputExtension}";
    public string OutputFullPath => Path.Combine(OutputPath, OutputFullName);
    public string Type { get; set; }

    public SourceFileVM(string source, bool isPrimary, InputVM input = null)
    {
        Input = input;
        IsPrimary = isPrimary;
        InputPath = Path.GetDirectoryName(source);
        InputName = Path.GetFileNameWithoutExtension(source);
        InputExtension = Path.GetExtension(source);
        InputFullPath = source;
        OutputPath = InputPath;
        OutputName = InputName + " - edit";
    }
}
