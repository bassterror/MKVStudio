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
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly VideoFileViewModel _video;
        private readonly AudioEditViewModel _audioEdit;
        private readonly IExternalLibrariesService _exLib;

        public RunLoudnormFirstPassCommand(VideoFileViewModel video, AudioEditViewModel audioEditViewModel, IExternalLibrariesService externalLibrariesService)
        {
            _video = video;
            _audioEdit = audioEditViewModel;
            _exLib = externalLibrariesService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            ProcessResult pr = await _exLib.Run(ProcessResultNames.LoudnormFirst, _video);
            _video.ProcessResults[pr.Name] = pr;
            SetLoudnormFirstPassMeasurements(_video.ProcessResults[ProcessResultNames.LoudnormFirst].StdErrOutput);
        }

        private void SetLoudnormFirstPassMeasurements(string firstPassOutput)
        {
            Match loudnormOutput = Regex.Match(firstPassOutput, @"(\{(\n*.*\n*)*?\})");
            JObject keyValuePairs = JObject.Parse(loudnormOutput.Value);
            _audioEdit.InputI = (string)keyValuePairs["input_i"];
            _audioEdit.InputTP = (string)keyValuePairs["input_tp"];
            _audioEdit.InputLRA = (string)keyValuePairs["input_lra"];
            _audioEdit.InputTresh = (string)keyValuePairs["input_thresh"];
            _audioEdit.OutputTP = (string)keyValuePairs["output_tp"];
            _audioEdit.OutputLRA = (string)keyValuePairs["output_lra"];
            _audioEdit.OutputTresh = (string)keyValuePairs["output_thresh"];
            _audioEdit.NormalizationType = (string)keyValuePairs["normalization_type"];
            _audioEdit.TargetOffset = (string)keyValuePairs["target_offset"];
        }
    }
}
