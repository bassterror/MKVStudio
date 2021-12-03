using MKVStudio.Models;
using MKVStudio.Services;
using MKVStudio.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RunLoudnormFirstPassCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly VideoFileViewModel _video;
        private readonly IExternalLibrariesService _exLib;

        public RunLoudnormFirstPassCommand(VideoFileViewModel video, IExternalLibrariesService externalLibrariesService)
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
            ProcessResult pr = await _exLib.Run(_video, ProcessResultNames.LoudnormFirst);
            _video.ProcessResults[pr.Name] = pr;
            SetLoudnormFirstPassMeasurements(_video.ProcessResults[ProcessResultNames.LoudnormFirst].StdErrOutput);
        }

        private void SetLoudnormFirstPassMeasurements(string firstPassOutput)
        {
            Match loudnormOutput = Regex.Match(firstPassOutput, @"(\{(\n*.*\n*)*?\})");
            JObject keyValuePairs = JObject.Parse(loudnormOutput.Value);
            _video.InputI = (string)keyValuePairs["input_i"];
            _video.InputTP = (string)keyValuePairs["input_tp"];
            _video.InputLRA = (string)keyValuePairs["input_lra"];
            _video.InputTresh = (string)keyValuePairs["input_thresh"];
            _video.OutputTP = (string)keyValuePairs["output_tp"];
            _video.OutputLRA = (string)keyValuePairs["output_lra"];
            _video.OutputTresh = (string)keyValuePairs["output_thresh"];
            _video.NormalizationType = (string)keyValuePairs["normalization_type"];
            _video.TargetOffset = (string)keyValuePairs["target_offset"];
        }
    }
}
