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
        private readonly Video _video;
        private readonly IExternalLibrariesService _exLib;

        public event EventHandler CanExecuteChanged;

        public RunMKVMergeCommand(Video video, IExternalLibrariesService externalLibrariesService)
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
            SetMeasurements(_video.ProcessResults[ProcessResultNames.MKVMerge].StdOutput);
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

        private void SetMeasurements(string mkvInfoOutput)
        {
            MKVMergeJ result = JsonConvert.DeserializeObject<MKVMergeJ>(mkvInfoOutput);
            _video.Title = result.Container.Properties.Title;
            _video.Channels = result.Tracks.First(t => t.Type == "audio").Properties.Audio_channels.ToString();
        }
    }
}
