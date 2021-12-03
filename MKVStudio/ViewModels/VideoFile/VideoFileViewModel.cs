using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using MKVStudio.State;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class VideoFileViewModel : BaseViewModel
    {
        #region Navigation
        public VideoFileViewModel ThisVideoFileViewModel { get; set; }
        private FileOverviewViewModel FileOverviewViewModel { get; set; }
        private TracksViewModel TracksViewModel { get; set; }
        private ConvertViewModel ConvertViewModel { get; set; }
        public BaseViewModel CurrentVideoFileViewModel { get; set; }
        public ICommand UpdateCurrentVideoFileViewModelCommand { get; set; }
        #endregion

        private readonly IExternalLibrariesService _exLib;

        public Dictionary<ProcessResultNames, ProcessResult> ProcessResults { get; private set; } = new();
        public string Title { get; set; }
        public ObservableCollection<VideoTrack> VideoTracks { get; set; } = new();
        public ObservableCollection<AudioTrack> AudioTracks { get; set; } = new();
        public ObservableCollection<SubtitleTrack> SubtitleTracks { get; set; } = new();

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
        #endregion

        public VideoFileViewModel(string source, IExternalLibrariesService externalLibrariesService)
        {
            ThisVideoFileViewModel = this;

            InputPath = Path.GetDirectoryName(source);
            InputName = Path.GetFileNameWithoutExtension(source);
            InputExtension = Path.GetExtension(source);
            InputFullPath = source;
            _exLib = externalLibrariesService;
            OutputPath = InputPath;
            OutputName = InputName + " - edit";

            RunLoudnormFirstPassCommand = new RunLoudnormFirstPassCommand(this, _exLib);
            RunMKVInfoCommand = new RunMKVInfoCommand(this, _exLib);
            RunMKVExtractCommand = new RunMKVExtractCommand(this, _exLib);

            FileOverviewViewModel = new FileOverviewViewModel(this);
            TracksViewModel = new TracksViewModel(this);
            ConvertViewModel = new ConvertViewModel(this);
            UpdateCurrentVideoFileViewModelCommand = new UpdateCurrentVideoFileViewModelCommand(this, FileOverviewViewModel, TracksViewModel, ConvertViewModel);
            UpdateCurrentVideoFileViewModelCommand.Execute(ViewModelTypes.FileOverview);

            CallMKVMergeJ();
        }

        private async void CallMKVMergeJ()
        {
            ProcessResult pr = await _exLib.Run(this, ProcessResultNames.MKVMergeJ);
            ProcessResults[ProcessResultNames.MKVMergeJ] = pr;
            SetVideoFileProperties(ProcessResults[ProcessResultNames.MKVMergeJ].StdOutput);
        }

        private void SetVideoFileProperties(string mkvMergeOutput)
        {
            MKVMergeJ result = JsonConvert.DeserializeObject<MKVMergeJ>(mkvMergeOutput);
            Title = result.Container.Properties.Title;
            foreach (MKVMergeJ.Track videoTrack in result.Tracks.Where(v => v.Type == "video"))
            {
                //_video.VideoTracks.Add(new VideoTrack());
            }
            foreach (MKVMergeJ.Track audioTrack in result.Tracks.Where(v => v.Type == "audio"))
            {
                AudioTracks.Add(new AudioTrack(audioTrack));
            }
            foreach (MKVMergeJ.Track subtitleTrack in result.Tracks.Where(v => v.Type == "subtitles"))
            {
                //_video.SubtitleTracks.Add(new SubtitleTrack());
            }
        }
    }
}
