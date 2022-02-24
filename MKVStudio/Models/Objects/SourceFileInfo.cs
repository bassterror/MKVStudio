using MKVStudio.Services;
using System.IO;

namespace MKVStudio.Models;

public class SourceFileInfo : BaseModel
{
    private readonly IUtilitiesService _util;
    private string _outputPath;
    private string _outputName;
    private string _outputNameSuffix;

    public bool IsPrimary { get; set; }
    public string InputPath { get; set; }
    public string InputName { get; set; }
    public string InputExtension { get; set; }
    public string InputFullName => InputName + InputExtension;
    public string InputFullPath { get; set; }
    public string OutputPath
    {
        get => _outputPath;
        set
        {
            _outputPath = value;
            OutputFullPath = Path.Combine(OutputPath, string.IsNullOrWhiteSpace(OutputFullName) ? string.Empty : OutputFullName);
        }
    }
    public string OutputName
    {
        get => _outputName;
        set
        {
            _outputName = value;
            OutputFullName = $"{OutputName}{OutputNameSuffix}{OutputExtension}";
            OutputFullPath = Path.Combine(OutputPath, string.IsNullOrWhiteSpace(OutputFullName) ? string.Empty : OutputFullName);
        }
    }
    public string OutputNameSuffix
    {
        get => _outputNameSuffix;
        set
        {
            _outputNameSuffix = value;
            OutputFullName = $"{OutputName}{OutputNameSuffix}{OutputExtension}";
            OutputFullPath = Path.Combine(OutputPath, string.IsNullOrWhiteSpace(OutputFullName) ? string.Empty : OutputFullName);
        }
    }
    public static string OutputExtension => ".mkv";
    public string OutputFullName { get; set; }
    public string OutputFullPath { get; set; }
    public string Type { get; set; }

    public SourceFileInfo(IUtilitiesService util, string source, bool isPrimary)
    {
        _util = util;
        IsPrimary = isPrimary;
        InputPath = Path.GetDirectoryName(source);
        InputName = Path.GetFileNameWithoutExtension(source);
        InputExtension = Path.GetExtension(source);
        InputFullPath = source;
        if (isPrimary)
        {
            if (util.Preferences.Destination.SameDirectoryAsTheFirstSourceFile)
            {
                OutputPath = InputPath;
            }
            if (util.Preferences.Destination.PreviouslyUsedDestinationDirectory)
            {
                //TODO
            }
            if (util.Preferences.Destination.DirectoryRelativeToFirstSourceFileDirectory)
            {
                //TODO
            }
            if (util.Preferences.Destination.UseFixedDirectory)
            {
                //TODO
            }
        }
    }
}
