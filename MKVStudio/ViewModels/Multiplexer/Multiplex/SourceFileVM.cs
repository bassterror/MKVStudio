﻿using System.IO;

namespace MKVStudio.ViewModels
{
    public class SourceFileVM : BaseViewModel
    {
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

        public SourceFileVM(string source)
        {
            InputPath = Path.GetDirectoryName(source);
            InputName = Path.GetFileNameWithoutExtension(source);
            InputExtension = Path.GetExtension(source);
            InputFullPath = source;
            OutputPath = InputPath;
            OutputName = InputName + " - edit";
        }
    }
}