﻿using Microsoft.Win32;
using MKVStudio.Models;
using MKVStudio.ViewModels;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MKVStudio.Services;

public class ExternalLibrariesService : IExternalLibrariesService
{
    public async Task<ProcessResult> Run(ProcessResultNames processName, SourceFileVM sourceFile = null, string attachmentId = null, string attachmentTempPath = null)
    {
        ProcessResult pr = new();

        switch (processName)
        {
            case ProcessResultNames.LoudnormFirst:
                pr = await RunProcess(Executables.FFmpeg, BuildArguments(processName, sourceFile), processName);
                break;
            case ProcessResultNames.LoudnormSecondStereo:
                pr = await RunProcess(Executables.FFmpeg, BuildArguments(processName, sourceFile), processName);
                break;
            case ProcessResultNames.LoudnormSecond6Channels:
                pr = await RunProcess(Executables.FFmpeg, BuildArguments(processName, sourceFile), processName);
                break;
            case ProcessResultNames.MKVInfo:
                pr = await RunProcess(Executables.MKVInfo, BuildArguments(processName, sourceFile), processName);
                break;
            case ProcessResultNames.MKVExtractAttachments:
                pr = await RunProcess(Executables.MKVExtract, BuildArguments(processName, sourceFile, attachmentId, attachmentTempPath), processName);
                break;
            case ProcessResultNames.MKVMergeJ:
                pr = await RunProcess(Executables.MKVMerge, BuildArguments(processName, sourceFile), processName);
                break;
            case ProcessResultNames.MKVMergeLangList:
                pr = await RunProcess(Executables.MKVMerge, BuildArguments(processName), processName);
                break;
            case ProcessResultNames.MKVMergeSupportedFileTypes:
                pr = await RunProcess(Executables.MKVMerge, BuildArguments(processName), processName);
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
            process.StartInfo.FileName = GetExecutable(executable);
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
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

    private static string BuildArguments(ProcessResultNames processName, SourceFileVM sourceFile = null, string attachmentId = null, string attachmentTempPath = null)
    {
        string arguments = string.Empty;
        switch (processName)
        {
            case ProcessResultNames.LoudnormFirst:
                arguments = $"-i \"{sourceFile.InputFullPath}\" -af loudnorm=I=-16:TP=-1.5:LRA=11:print_format=json -f null -";
                break;
            case ProcessResultNames.LoudnormSecondStereo:
                //arguments = $"-i \"{video.InputFullPath}\" -af loudnorm=I=-16:LRA=11:TP=-1.5 aresample={video.SampleRates} \"{video.OutputFullPath}\"";
                break;
            case ProcessResultNames.LoudnormSecond6Channels:
                //arguments = $"-i \"{video.InputFullPath}\" -filter_complex \"[0:a:0]channelsplit=channel_layout=5.1[FL][FR][FC][LFE][BL][BR];[FC]loudnorm=I=-16:TP=-1.5:LRA=11:measured_I={video.InputI}:measured_LRA={video.InputLRA}:measured_TP={video.InputTP}:measured_thresh={video.InputTresh}:offset={video.TargetOffset}:linear=true,aformat=sample_rates={video.SampleRates}:channel_layouts=mono[FC2];[FL]aformat=channel_layouts=mono[FL2];[FR]aformat=channel_layouts=mono[FR2];[LFE]aformat=channel_layouts=mono[LFE2];[BL]aformat=channel_layouts=mono[BL2];[BR]aformat=channel_layouts=mono[BR2];[FL2][FR2][FC2][LFE2][BL2][BR2]join=inputs=6:channel_layout=5.1:map=0.0-FL|1.0-FR|2.0-FC|3.0-LFE|4.0-BL|5.0-BR\" -c:v copy -c:a libfdk_aac -vbr 3 -c:s copy \"{video.OutputFullPath}\"";
                break;
            case ProcessResultNames.MKVInfo:
                arguments = $"\"{sourceFile.InputFullPath}\"";
                break;
            case ProcessResultNames.MKVExtractAttachments:
                arguments = $"\"{sourceFile.InputFullPath}\" attachments {attachmentId}:\"{attachmentTempPath}\"";
                break;
            case ProcessResultNames.MKVMergeJ:
                arguments = $"-J \"{sourceFile.InputFullPath}\"";
                break;
            case ProcessResultNames.MKVMergeLangList:
                arguments = "--list-languages";
                break;
            case ProcessResultNames.MKVMergeSupportedFileTypes:
                arguments = "-l";
                break;
        }

        return arguments;
    }

    public enum Executables
    {
        FFmpeg,
        MKVInfo,
        MKVMerge,
        MKVPropEdit,
        MKVExtract
    }

    public string GetExecutable(Executables executable)
    {
        using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);
        string path = string.Empty;
        switch (executable)
        {
            case Executables.FFmpeg:
                path = key.GetValue(Executables.FFmpeg.ToString()).ToString();
                break;
            case Executables.MKVInfo:
                path = key.GetValue(Executables.MKVInfo.ToString()).ToString();
                break;
            case Executables.MKVMerge:
                path = key.GetValue(Executables.MKVMerge.ToString()).ToString();
                break;
            case Executables.MKVPropEdit:
                path = key.GetValue(Executables.MKVPropEdit.ToString()).ToString();
                break;
            case Executables.MKVExtract:
                path = key.GetValue(Executables.MKVExtract.ToString()).ToString();
                break;
        }
        return path;
    }

    private void SetLoudnormFirstPassMeasurements(string firstPassOutput)
    {
        Match loudnormOutput = Regex.Match(firstPassOutput, @"(\{(\n*.*\n*)*?\})");
        JObject keyValuePairs = JObject.Parse(loudnormOutput.Value);
        //_audioEdit.InputI = (string)keyValuePairs["input_i"];
        //_audioEdit.InputTP = (string)keyValuePairs["input_tp"];
        //_audioEdit.InputLRA = (string)keyValuePairs["input_lra"];
        //_audioEdit.InputTresh = (string)keyValuePairs["input_thresh"];
        //_audioEdit.OutputTP = (string)keyValuePairs["output_tp"];
        //_audioEdit.OutputLRA = (string)keyValuePairs["output_lra"];
        //_audioEdit.OutputTresh = (string)keyValuePairs["output_thresh"];
        //_audioEdit.NormalizationType = (string)keyValuePairs["normalization_type"];
        //_audioEdit.TargetOffset = (string)keyValuePairs["target_offset"];
    }
}
