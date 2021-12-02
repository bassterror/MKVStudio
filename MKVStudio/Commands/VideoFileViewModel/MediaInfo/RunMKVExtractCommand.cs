using MKVStudio.Models;
using MKVStudio.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using static MKVStudio.Services.UtilitiesService;

namespace MKVStudio.Commands
{
    public class RunMKVExtractCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Video _video;
        private readonly IExternalLibrariesService _exLib;

        public RunMKVExtractCommand(Video video, IExternalLibrariesService externalLibrariesService)
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
            ProcessResult pr = await _exLib.RunProcess(Executables.MKVExtract, BuildArguments(ProcessResultNames.MKVExtract), ProcessResultNames.MKVExtract);
            _video.ProcessResults[ProcessResultNames.MKVExtract] = pr;
            //SetMeasurements(_video.ProcessResults[ProcessResultNames.MKVExtract].StdErrOutput);
        }

        private string BuildArguments(ProcessResultNames processName)
        {
            string arguments = string.Empty;
            switch (processName)
            {
                case ProcessResultNames.MKVExtract:
                    arguments = $"\"{_video.InputFullPath}\" tracks";
                    break;
                default:
                    break;
            }

            return arguments;
        }
    }
}
