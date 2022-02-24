﻿using MKVStudio.Services;
using System.IO;

namespace MKVStudio.Models;

public class SourceFileInfo : BaseModel
{
    private readonly IUtilitiesService _util;

    public bool IsPrimary { get; set; }
    public string InputPath { get; set; }
    public string InputName { get; set; }
    public string InputExtension { get; set; }
    public string InputFullName => InputName + InputExtension;
    public string InputFullPath { get; set; }
    public string OutputPath { get; set; }
    public string OutputName { get; set; }
    public string OutputNameSuffix { get; set; }
    public static string OutputExtension => ".mkv";
    public string OutputFullName => $"{OutputName}{OutputNameSuffix}{OutputExtension}";
    public string OutputFullPath => Path.Combine(OutputPath, OutputFullName);
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
            if (util.Preferences.OutputName.SameDirectoryAsTheFirstSourceFile)
            {
                OutputPath = InputPath;
            }
            if (util.Preferences.OutputName.PreviouslyUsedDestinationDirectory)
            {
                //TODO
            }
            if (util.Preferences.OutputName.DirectoryRelativeToFirstSourceFileDirectory)
            {
                //TODO
            }
            if (util.Preferences.OutputName.UseFixedDirectory)
            {
                //TODO
            }
        }
    }
}
