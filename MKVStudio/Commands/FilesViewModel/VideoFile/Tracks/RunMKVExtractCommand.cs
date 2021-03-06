using MKVStudio.Models;
using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RunMKVExtractCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly VideoFileVM _video;
        private readonly IExternalLibrariesService _exLib;

        public RunMKVExtractCommand(VideoFileVM video, IExternalLibrariesService externalLibrariesService)
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
            ProcessResult pr = await _exLib.Run(ProcessResultNames.MKVExtract, _video);
            _video.ProcessResults[ProcessResultNames.MKVExtract] = pr;
            //SetMeasurements(_video.ProcessResults[ProcessResultNames.MKVExtract].StdErrOutput);
        }
    }
}
