using MKVStudio.Handlers;
using MKVStudio.ViewModels;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MKVStudio.Data
{
    public class Video
    {
        public string InputPath { get; private set; }
        public string InputName { get; private set; }
        public string InputExtension { get; private set; }
        public string InputFullName { get; private set; }
        public string InputFullPath { get; private set; }
        public string OutputPath { get; private set; }
        public string OutputName { get; private set; }
        public string OutputExtension { get; private set; }
        public string OutputFullName { get; private set; }
        public string OutputFullPath { get; private set; }
        public List<ProcessResult> ProcessResults { get; private set; } = new();
        public string Channels { get; private set; }
        public string InputI { get; private set; }
        public string InputTP { get; private set; }
        public string InputLRA { get; private set; }
        public string InputTresh { get; private set; }
        public string OutputTP { get; private set; }
        public string OutputLRA { get; private set; }
        public string OutputTresh { get; private set; }
        public string NormalizationType { get; private set; }
        public string TargetOffset { get; private set; }
        public string SampleRates { get; private set; }
        public ICommand RunFirstPassCommand { get; set; }

        public Video(string source)
        {
            InputPath = Path.GetDirectoryName(source);
            InputName = Path.GetFileNameWithoutExtension(source);
            InputExtension = Path.GetExtension(source);
            InputFullName = InputName + InputExtension;
            InputFullPath = source;
            OutputPath = InputPath;
            OutputName = InputName + " - edit";
            OutputExtension = InputExtension;
            OutputFullName = $"{OutputName}.{OutputExtension}";
            OutputFullPath = Path.Combine(OutputPath, OutputFullName);
            RunFirstPassCommand = new RelayCommand(RunFirstPass);
        }

        private async void RunFirstPass()
        {
            FfmpegHandler ffmpegHandler = new();
            ProcessResult pr = await ffmpegHandler.RunFFMPEG(BuildArguments("firstPass"), "firstPass");
            ProcessResults.Add(pr);
            SetMeasurements(ProcessResults.First(p => p.Name == "firstPass").StdErrOutput);
        }

        public string BuildArguments(string processName)
        {
            string arguments = string.Empty;
            switch (processName)
            {
                case "firstPass":
                    arguments = $"-i \"{InputFullPath}\" -af loudnorm=I=-16:TP=-1.5:LRA=11:print_format=json -f null -";
                    break;
                case "secondPassStereo":
                    arguments = $"-i \"{InputFullPath}\" -af loudnorm=I=-16:LRA=11:TP=-1.5 aresample={SampleRates} \"{OutputFullPath}\"";
                    break;
                case "secondPass5.1":
                    arguments = $"-i \"{InputFullPath}\" -filter_complex \"[0:a:0]channelsplit=channel_layout=5.1[FL][FR][FC][LFE][BL][BR];[FC]loudnorm=I=-16:TP=-1.5:LRA=11:measured_I={InputI}:measured_LRA={InputLRA}:measured_TP={InputTP}:measured_thresh={InputTresh}:offset={TargetOffset}:linear=true,aformat=sample_rates={SampleRates}:channel_layouts=mono[FC2];[FL]aformat=channel_layouts=mono[FL2];[FR]aformat=channel_layouts=mono[FR2];[LFE]aformat=channel_layouts=mono[LFE2];[BL]aformat=channel_layouts=mono[BL2];[BR]aformat=channel_layouts=mono[BR2];[FL2][FR2][FC2][LFE2][BL2][BR2]join=inputs=6:channel_layout=5.1:map=0.0-FL|1.0-FR|2.0-FC|3.0-LFE|4.0-BL|5.0-BR\" -c:v copy -c:a libfdk_aac -vbr 3 -c:s copy \"{OutputFullPath}\"";
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
            InputI = (string)keyValuePairs["input_i"];
            InputTP = (string)keyValuePairs["input_tp"];
            InputLRA = (string)keyValuePairs["input_lra"];
            InputTresh = (string)keyValuePairs["input_thresh"];
            OutputTP = (string)keyValuePairs["output_tp"];
            OutputLRA = (string)keyValuePairs["output_lra"];
            OutputTresh = (string)keyValuePairs["output_thresh"];
            NormalizationType = (string)keyValuePairs["normalization_type"];
            TargetOffset = (string)keyValuePairs["target_offset"];
            Match audioDetails = Regex.Match(firstPassOutput, @"Audio:.*,\s(\d*)\sHz,\s(\w*).*,");
            SampleRates = audioDetails.Groups[1].ToString();
            Channels = audioDetails.Groups[2].ToString();
        }
    }
}
