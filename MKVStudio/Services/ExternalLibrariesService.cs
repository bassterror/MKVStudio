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
        private IUtilitiesService _util;
        public IUtilitiesService Util
        {
            get  => _util;
            set
            {
                _util = value;
            }
        }
        private readonly IFfmpegService _ffmpeg;
        private readonly IMkvToolNixService _mkvToolNix;

        public ExternalLibrariesService(IUtilitiesService utilitiesService, IFfmpegService ffmpegService, IMkvToolNixService mkvToolNixService)
        {
            Util = utilitiesService;
            _ffmpeg = ffmpegService;
            _mkvToolNix = mkvToolNixService;
        }


        public async Task<ProcessResult> RunProcess(Executables executable, string arguments, ProcessResultNames processName)
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

    }
}
