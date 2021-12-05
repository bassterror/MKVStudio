using MKVStudio.Models;
using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RunMKVInfoCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly VideoFileViewModel _video;
        private readonly IExternalLibrariesService _exLib;


        public RunMKVInfoCommand(VideoFileViewModel video, IExternalLibrariesService externalLibrariesService)
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
            ProcessResult pr = await _exLib.Run(_video, ProcessResultNames.MKVInfo);
            _video.ProcessResults[ProcessResultNames.MKVInfo] = pr;
            SetMeasurements(_video.ProcessResults[ProcessResultNames.MKVInfo].StdOutput);
        }

        private void SetMeasurements(string mkvInfoOutput)
        {
            Match match = Regex.Match(mkvInfoOutput, @"^.+\+\sTitle:\s(.+)$", RegexOptions.Multiline);
            //_video.Title = match.Groups[1].ToString();
            //JObject keyValuePairs = JObject.Parse(loudnormOutput.Value);
            //_video.InputI = (string)keyValuePairs["input_i"];
            //Match audioDetails = Regex.Match(firstPassOutput, @"Audio:.*,\s(\d*)\sHz,\s(\w*).*,");
            //_video.SampleRates = audioDetails.Groups[1].ToString();
            //_video.Channels = audioDetails.Groups[2].ToString();
        }
    }
}
