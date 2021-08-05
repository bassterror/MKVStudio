using MKVStudio.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MKVStudio
{
    public class FfmpegHandler
    {
        private readonly string ffmpegPath = @"C:\Program Files\FFA\local64\bin-video\ffmpeg.exe";

        public async Task<ProcessResult> RunFFMPEG(string arguments, string processName)
        {
            ProcessResult processResult = new();

            using (Process process = new())
            {
                List<Task> processTasks = new();
                TaskCompletionSource<object> processExitEvent = new();
                process.Exited += (s, e) =>
                {
                    processExitEvent.TrySetResult(true);
                };
                processTasks.Add(processExitEvent.Task);

                process.EnableRaisingEvents = true;
                process.StartInfo.FileName = ffmpegPath;
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
                        stdOutCloseEvent.TrySetResult(true);
                    }
                    else
                    {
                        output.AppendLine(e.Data);
                    }
                };
                processTasks.Add(stdOutCloseEvent.Task);

                StringBuilder errorOutput = new();
                TaskCompletionSource<bool> stdErrCloseEvent = new();
                process.ErrorDataReceived += (s, e) =>
                {
                    if (e.Data == null)
                    {
                        stdErrCloseEvent.TrySetResult(true);
                    }
                    else
                    {
                        errorOutput.AppendLine(e.Data);
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
