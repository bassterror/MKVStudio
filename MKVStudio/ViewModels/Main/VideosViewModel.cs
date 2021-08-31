using MKVStudio.Data;
using MKVStudio.Handlers;
using MKVStudio.ViewModels.Base;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MKVStudio.ViewModels.Main
{
    public class VideosViewModel : BaseMainViewModel
    {
        public ObservableCollection<VideosViewModel> Videos { get; set; } = new();
        public ICommand RunFirstPassCommand { get; set; }

        public VideosViewModel()
        {
            RunFirstPassCommand = new RelayCommand(RunFirstPass);

            //RunFirstPass();
        }

        private async void RunFirstPass()
        {
            FfmpegHandler ffmpegHandler = new();
            ProcessResult pr = await ffmpegHandler.RunFFMPEG(BuildArguments("firstPass"), "firstPass");
            ProcessResults.Add(pr);
            SetMeasurements(ProcessResults.First(p => p.Name == "firstPass").StdErrOutput);
        }

        #region Help
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
        #endregion
    }
}
