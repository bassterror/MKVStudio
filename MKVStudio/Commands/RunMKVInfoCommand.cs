using MKVStudio.Models;
using MKVStudio.Services;
using System;
using System.Windows.Input;
using static MKVStudio.Services.RegistryService;

namespace MKVStudio.Commands
{
    public class RunMKVInfoCommand : ICommand
    {
        private readonly Video _video;
        private readonly IExternalLibrariesService _exLib;

        public event EventHandler CanExecuteChanged;

        public RunMKVInfoCommand(Video video, IExternalLibrariesService externalLibrariesService)
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
            ProcessResult pr = await _exLib.RunProcess(Executables.MKVInfo, BuildArguments(ProcessResultNames.MKVInfo), ProcessResultNames.MKVInfo);
            _video.ProcessResults[ProcessResultNames.MKVInfo] = pr;
            //SetMeasurements(_video.ProcessResults.First(p => p.Name == "firstPass").StdErrOutput);
        }

        private string BuildArguments(ProcessResultNames processName)
        {
            string arguments = string.Empty;
            switch (processName)
            {
                case ProcessResultNames.MKVInfo:
                    arguments = $"\"{_video.InputFullPath}\"";
                    break;
                default:
                    break;
            }

            return arguments;
        }
    }
}
