using MKVStudio.Models;
using MKVStudio.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using static MKVStudio.Services.UtilitiesService;

namespace MKVStudio.Commands
{
    public class RunMKVInfoCommand : ICommand
    {
        private readonly VideoFile _video;
        private readonly IExternalLibrariesService _exLib;

        public event EventHandler CanExecuteChanged;

        public RunMKVInfoCommand(VideoFile video, IExternalLibrariesService externalLibrariesService)
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
            ProcessResult pr = await _exLib.RunProcess(Executables.MKVInfo, BuildArguments(ProcessResultNames.MKVInfo), ProcessResultNames.MKVInfo);
            _video.ProcessResults[ProcessResultNames.MKVInfo] = pr;
            SetMeasurements(_video.ProcessResults[ProcessResultNames.MKVInfo].StdOutput);
        }

        private string BuildArguments(ProcessResultNames processName)
        {
            string arguments = string.Empty;
            switch (processName)
            {
                case ProcessResultNames.MKVInfo:
                    arguments = $"\"{_video.InputFullPath}\"";
                    break;
                default:
                    break;
            }

            return arguments;
        }

        private void SetMeasurements(string mkvInfoOutput)
        {
            Match match = Regex.Match(mkvInfoOutput, @"^.+\+\sTitle:\s(.+)$", RegexOptions.Multiline);
            _video.Title = match.Groups[1].ToString();
            //JObject keyValuePairs = JObject.Parse(loudnormOutput.Value);
            //_video.InputI = (string)keyValuePairs["input_i"];
            //Match audioDetails = Regex.Match(firstPassOutput, @"Audio:.*,\s(\d*)\sHz,\s(\w*).*,");
            //_video.SampleRates = audioDetails.Groups[1].ToString();
            //_video.Channels = audioDetails.Groups[2].ToString();
        }
    }
}
