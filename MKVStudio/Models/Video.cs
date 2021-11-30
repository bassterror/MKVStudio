using MKVStudio.Commands;
using MKVStudio.Services;
using PropertyChanged;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace MKVStudio.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Video : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        private readonly IFfmpegService _ffmpeg;

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
        public string Channels { get; set; }
        public string InputI { get; set; }
        public string InputTP { get; set; }
        public string InputLRA { get; set; }
        public string InputTresh { get; set; }
        public string OutputTP { get; set; }
        public string OutputLRA { get; set; }
        public string OutputTresh { get; set; }
        public string NormalizationType { get; set; }
        public string TargetOffset { get; set; }
        public string SampleRates { get; set; }
        public ICommand RunFirstPassCommand { get; set; }

        public Video(string source, IFfmpegService ffmpegService)
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
            _ffmpeg = ffmpegService;
            RunFirstPassCommand = new RunFirstPassCommand(this, _ffmpeg);
        }
    }
}
