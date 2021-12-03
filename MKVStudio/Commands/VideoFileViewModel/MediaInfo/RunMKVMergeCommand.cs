using MKVStudio.Models;
using MKVStudio.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Windows.Input;
using static MKVStudio.Services.UtilitiesService;

namespace MKVStudio.Commands
{
    public class RunMKVMergeCommand : ICommand
    {
        private readonly VideoFile _video;
        private readonly IExternalLibrariesService _exLib;

        public event EventHandler CanExecuteChanged;

        public RunMKVMergeCommand(VideoFile video, IExternalLibrariesService externalLibrariesService)
        {
            _video = video;
            _exLib = externalLibrariesService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            ProcessResult pr = await _exLib.RunProcess(Executables.MKVMerge, BuildArguments(ProcessResultNames.MKVMerge), ProcessResultNames.MKVMerge);
            _video.ProcessResults[ProcessResultNames.MKVMerge] = pr;
            SetVideoFileProperties(_video.ProcessResults[ProcessResultNames.MKVMerge].StdOutput);
        }

        private string BuildArguments(ProcessResultNames processName)
        {
            string arguments = string.Empty;
            switch (processName)
            {
                case ProcessResultNames.MKVMerge:
                    arguments = $"-J \"{_video.InputFullPath}\"";
                    break;
                default:
                    break;
            }

            return arguments;
        }

        private void SetVideoFileProperties(string mkvMergeOutput)
        {
            MKVMergeJ result = JsonConvert.DeserializeObject<MKVMergeJ>(mkvMergeOutput);
            _video.Title = result.Container.Properties.Title;
            foreach (MKVMergeJ.Track videoTrack in result.Tracks.Where(v => v.Type == "video"))
            {
                //_video.VideoTracks.Add(new VideoTrack());
            }
            foreach (MKVMergeJ.Track audioTrack in result.Tracks.Where(v => v.Type == "audio"))
            {
                _video.AudioTracks.Add(new AudioTrack(audioTrack));
            }
            foreach (MKVMergeJ.Track subtitleTrack in result.Tracks.Where(v => v.Type == "subtitles"))
            {
                //_video.SubtitleTracks.Add(new SubtitleTrack());
            }
        }
    }
}
