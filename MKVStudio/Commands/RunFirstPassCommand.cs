using MKVStudio.Models;
using MKVStudio.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RunFirstPassCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Video _video;

        public RunFirstPassCommand(Video video)
        {
            _video = video;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            FfmpegService ffmpegHandler = new();
            ProcessResult pr = await ffmpegHandler.RunFFMPEG(BuildArguments("firstPass"), "firstPass");
            _video.ProcessResults.Add(pr);
            SetMeasurements(_video.ProcessResults.First(p => p.Name == "firstPass").StdErrOutput);
        }

        public string BuildArguments(string processName)
        {
            string arguments = string.Empty;
            switch (processName)
            {
                case "firstPass":
                    arguments = $"-i \"{_video.InputFullPath}\" -af loudnorm=I=-16:TP=-1.5:LRA=11:print_format=json -f null -";
                    break;
                case "secondPassStereo":
                    arguments = $"-i \"{_video.InputFullPath}\" -af loudnorm=I=-16:LRA=11:TP=-1.5 aresample={_video.SampleRates} \"{_video.OutputFullPath}\"";
                    break;
                case "secondPass5.1":
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
            Match audioDetails = Regex.Match(firstPassOutput, @"Audio:.*,\s(\d*)\sHz,\s(\w*).*,");
            _video.SampleRates = audioDetails.Groups[1].ToString();
            _video.Channels = audioDetails.Groups[2].ToString();
        }
    }
}
