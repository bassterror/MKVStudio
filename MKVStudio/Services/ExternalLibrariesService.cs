using MKVStudio.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using static MKVStudio.Services.UtilitiesService;

namespace MKVStudio.Services
{
    public class ExternalLibrariesService : IExternalLibrariesService
    {
        public IUtilitiesService Util { get; set; }
        private readonly IFfmpegService _ffmpeg;
        private readonly IMkvToolNixService _mkvToolNix;

        public ExternalLibrariesService(IUtilitiesService utilitiesService, IFfmpegService ffmpegService, IMkvToolNixService mkvToolNixService)
        {
            Util = utilitiesService;
            _ffmpeg = ffmpegService;
            _mkvToolNix = mkvToolNixService;
        }

        public async Task<ProcessResult> Run(VideoFile video, ProcessResultNames processName)
        {
            ProcessResult pr = new();

            switch (processName)
            {
                case ProcessResultNames.LoudnormFirst:
                    pr = await RunProcess(Executables.FFmpeg, BuildArguments(processName, video), processName);
                    break;
                case ProcessResultNames.LoudnormSecondStereo:
                    pr = await RunProcess(Executables.FFmpeg, BuildArguments(processName, video), processName);
                    break;
                case ProcessResultNames.LoudnormSecond6Channels:
                    pr = await RunProcess(Executables.FFmpeg, BuildArguments(processName, video), processName);
                    break;
                case ProcessResultNames.MKVInfo:
                    pr = await RunProcess(Executables.MKVInfo, BuildArguments(processName, video), processName);
                    break;
                case ProcessResultNames.MKVExtract:
                    pr = await RunProcess(Executables.MKVExtract, BuildArguments(processName, video), processName);
                    break;
                case ProcessResultNames.MKVMergeJ:
                    pr = await RunProcess(Executables.MKVMerge, BuildArguments(processName, video), processName);
                    break;
            }

            return pr;
        }

        private async Task<ProcessResult> RunProcess(Executables executable, string arguments, ProcessResultNames processName)
        {
            ProcessResult processResult = new();

            using (Process process = new())
            {
                List<Task> processTasks = new();
                TaskCompletionSource<object> processExitEvent = new();
                process.Exited += (s, e) =>
                {
                    _ = processExitEvent.TrySetResult(true);
                };
                processTasks.Add(processExitEvent.Task);
                process.EnableRaisingEvents = true;
                process.StartInfo.FileName = Util.GetExecutable(executable);
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                StringBuilder output = new();
                TaskCompletionSource<bool> stdOutCloseEvent = new();
                process.OutputDataReceived += (s, e) =>
                {
                    if (e.Data == null)
                    {
                        _ = stdOutCloseEvent.TrySetResult(true);
                    }
                    else
                    {
                        _ = output.AppendLine(e.Data);
                    }
                };
                processTasks.Add(stdOutCloseEvent.Task);

                StringBuilder errorOutput = new();
                TaskCompletionSource<bool> stdErrCloseEvent = new();
                process.ErrorDataReceived += (s, e) =>
                {
                    if (e.Data == null)
                    {
                        _ = stdErrCloseEvent.TrySetResult(true);
                    }
                    else
                    {
                        _ = errorOutput.AppendLine(e.Data);
                    }
                };
                processTasks.Add(stdErrCloseEvent.Task);

                if (!process.Start())
                {
                    processResult.ExitCode = process.ExitCode;
                    return processResult;
                }
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                Task processCompletionTask = Task.WhenAll(processTasks);
                Task<Task> awaitingTask = Task.WhenAny(processCompletionTask);

                if ((await awaitingTask.ConfigureAwait(false)) == processCompletionTask)
                {
                    processResult.ExitCode = process.ExitCode;
                }
                else
                {
                    try
                    {
                        process.Kill();
                    }
                    catch
                    {
                        // ignored
                    }
                }

                processResult.Name = processName;
                processResult.StdOutput = output.ToString();
                processResult.StdErrOutput = errorOutput.ToString();
            }

            return processResult;
        }

        private static string BuildArguments(ProcessResultNames processName, VideoFile video)
        {
            string arguments = string.Empty;
            switch (processName)
            {
                case ProcessResultNames.LoudnormFirst:
                    arguments = $"-i \"{video.InputFullPath}\" -af loudnorm=I=-16:TP=-1.5:LRA=11:print_format=json -f null -";
                    break;
                case ProcessResultNames.LoudnormSecondStereo:
                    //arguments = $"-i \"{video.InputFullPath}\" -af loudnorm=I=-16:LRA=11:TP=-1.5 aresample={video.SampleRates} \"{video.OutputFullPath}\"";
                    break;
                case ProcessResultNames.LoudnormSecond6Channels:
                    //arguments = $"-i \"{video.InputFullPath}\" -filter_complex \"[0:a:0]channelsplit=channel_layout=5.1[FL][FR][FC][LFE][BL][BR];[FC]loudnorm=I=-16:TP=-1.5:LRA=11:measured_I={video.InputI}:measured_LRA={video.InputLRA}:measured_TP={video.InputTP}:measured_thresh={video.InputTresh}:offset={video.TargetOffset}:linear=true,aformat=sample_rates={video.SampleRates}:channel_layouts=mono[FC2];[FL]aformat=channel_layouts=mono[FL2];[FR]aformat=channel_layouts=mono[FR2];[LFE]aformat=channel_layouts=mono[LFE2];[BL]aformat=channel_layouts=mono[BL2];[BR]aformat=channel_layouts=mono[BR2];[FL2][FR2][FC2][LFE2][BL2][BR2]join=inputs=6:channel_layout=5.1:map=0.0-FL|1.0-FR|2.0-FC|3.0-LFE|4.0-BL|5.0-BR\" -c:v copy -c:a libfdk_aac -vbr 3 -c:s copy \"{video.OutputFullPath}\"";
                    break;
                case ProcessResultNames.MKVInfo:
                    arguments = $"\"{video.InputFullPath}\"";
                    break;
                case ProcessResultNames.MKVExtract:
                    arguments = $"\"{video.InputFullPath}\" tracks";
                    break;
                case ProcessResultNames.MKVMergeJ:
                    arguments = $"-J \"{video.InputFullPath}\"";
                    break;
                default:
                    break;
            }

            return arguments;
        }


    }
}
