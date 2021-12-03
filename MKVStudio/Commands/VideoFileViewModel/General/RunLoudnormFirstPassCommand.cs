using MKVStudio.Models;
using MKVStudio.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using static MKVStudio.Services.UtilitiesService;

namespace MKVStudio.Commands
{
    public class RunLoudnormFirstPassCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly VideoFile _video;
        private readonly IExternalLibrariesService _exLib;

        public RunLoudnormFirstPassCommand(VideoFile video, IExternalLibrariesService externalLibrariesService)
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
            ProcessResult pr = await _exLib.RunProcess(Executables.FFmpeg, BuildArguments(ProcessResultNames.LoudnormFirst), ProcessResultNames.LoudnormFirst);
            _video.ProcessResults[pr.Name] = pr;
            SetMeasurements(_video.ProcessResults[ProcessResultNames.LoudnormFirst].StdErrOutput);
        }

        private string BuildArguments(ProcessResultNames processName)
        {
            string arguments = string.Empty;
            switch (processName)
            {
                case ProcessResultNames.LoudnormFirst:
                    arguments = $"-i \"{_video.InputFullPath}\" -af loudnorm=I=-16:TP=-1.5:LRA=11:print_format=json -f null -";
                    break;
                case ProcessResultNames.LoudnormSecondStereo:
                    arguments = $"-i \"{_video.InputFullPath}\" -af loudnorm=I=-16:LRA=11:TP=-1.5 aresample={_video.SampleRates} \"{_video.OutputFullPath}\"";
                    break;
                case ProcessResultNames.LoudnormSecond6Channels:
                    arguments = $"-i \"{_video.InputFullPath}\" -filter_complex \"[0:a:0]channelsplit=channel_layout=5.1[FL][FR][FC][LFE][BL][BR];[FC]loudnorm=I=-16:TP=-1.5:LRA=11:measured_I={_video.InputI}:measured_LRA={_video.InputLRA}:measured_TP={_video.InputTP}:measured_thresh={_video.InputTresh}:offset={_video.TargetOffset}:linear=true,aformat=sample_rates={_video.SampleRates}:channel_layouts=mono[FC2];[FL]aformat=channel_layouts=mono[FL2];[FR]aformat=channel_layouts=mono[FR2];[LFE]aformat=channel_layouts=mono[LFE2];[BL]aformat=channel_layouts=mono[BL2];[BR]aformat=channel_layouts=mono[BR2];[FL2][FR2][FC2][LFE2][BL2][BR2]join=inputs=6:channel_layout=5.1:map=0.0-FL|1.0-FR|2.0-FC|3.0-LFE|4.0-BL|5.0-BR\" -c:v copy -c:a libfdk_aac -vbr 3 -c:s copy \"{_video.OutputFullPath}\"";
                    break;
                default:
                    break;
            }

            return arguments;
        }

        private void SetMeasurements(string firstPassOutput)
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
