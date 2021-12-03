using MKVStudio.Commands;
using MKVStudio.Services;
using PropertyChanged;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.Models
{
    [AddINotifyPropertyChangedInterface]
    public class VideoFile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private readonly IExternalLibrariesService _exLib;

        public Dictionary<ProcessResultNames, ProcessResult> ProcessResults { get; private set; } = new();
        public string Title { get; set; }
        public List<VideoTrack> VideoTracks { get; set; }
        public List<AudioTrack> AudioTracks { get; set; }
        public List<SubtitleTrack> SubtitleTracks { get; set; }

        #region I/O
        public string InputPath { get; private set; }
        public string InputName { get; private set; }
        public string InputExtension { get; private set; }
        public string InputFullName => InputName + InputExtension;
        public string InputFullPath { get; private set; }
        public string OutputPath { get; set; }
        public string OutputName { get; set; }
        public static string OutputExtension => ".mkv";
        public string OutputFullName => $"{OutputName}.{OutputExtension}";
        public string OutputFullPath => Path.Combine(OutputPath, OutputFullName);
        #endregion

        #region LoudnormFirstPass
        public string InputI { get; set; }
        public string InputTP { get; set; }
        public string InputLRA { get; set; }
        public string InputTresh { get; set; }
        public string OutputTP { get; set; }
        public string OutputLRA { get; set; }
        public string OutputTresh { get; set; }
        public string NormalizationType { get; set; }
        public string TargetOffset { get; set; }
        #endregion

        #region Commands
        public ICommand RunLoudnormFirstPassCommand { get; set; }
        public ICommand RunMKVInfoCommand { get; set; }
        public ICommand RunMKVExtractCommand { get; set; }
        public ICommand RunMKVMergeCommand { get; set; }
        #endregion

        public VideoFile(string source, IExternalLibrariesService externalLibrariesService)
        {
            InputPath = Path.GetDirectoryName(source);
            InputName = Path.GetFileNameWithoutExtension(source);
            InputExtension = Path.GetExtension(source);
            InputFullPath = source;
            OutputPath = InputPath;
            OutputName = InputName + " - edit";
            _exLib = externalLibrariesService;
            RunLoudnormFirstPassCommand = new RunLoudnormFirstPassCommand(this, _exLib);
            RunMKVInfoCommand = new RunMKVInfoCommand(this, _exLib);
            RunMKVExtractCommand = new RunMKVExtractCommand(this, _exLib);
            RunMKVMergeCommand = new RunMKVMergeCommand(this, _exLib);
            RunMKVMergeCommand.Execute(null);
        }
    }
}
