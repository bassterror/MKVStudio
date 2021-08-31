using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKVStudio.Data
{
    public class Video
    {
        public string InputPath { get; private set; }
        public string InputName { get; private set; }
        public string InputExtension { get; private set; }
        public string InputFullName { get; private set; }
        public string InputFullPath { get; private set; }
        public string OutputPath { get; private set; }
        public string OutputName { get; private set; }
        public string OutputExtension { get; private set; }
        public string OutputFullName { get; private set; }
        public string OutputFullPath { get; private set; }
        public List<ProcessResult> ProcessResults { get; private set; } = new();
        public string Channels { get; private set; }
        public string InputI { get; private set; }
        public string InputTP { get; private set; }
        public string InputLRA { get; private set; }
        public string InputTresh { get; private set; }
        public string OutputTP { get; private set; }
        public string OutputLRA { get; private set; }
        public string OutputTresh { get; private set; }
        public string NormalizationType { get; private set; }
        public string TargetOffset { get; private set; }
        public string SampleRates { get; private set; }

        public Video(string source)
        {
            InputPath = Path.GetDirectoryName(source);
            InputName = Path.GetFileNameWithoutExtension(source);
            InputExtension = Path.GetExtension(source);
            InputFullName = InputName + InputExtension;
            InputFullPath = source;
            OutputPath = InputPath;
            OutputName = InputName + " - edit";
            OutputExtension = InputExtension;
            OutputFullName = $"{OutputName}.{OutputExtension}";
            OutputFullPath = Path.Combine(OutputPath, OutputFullName);
        }
    }
}
