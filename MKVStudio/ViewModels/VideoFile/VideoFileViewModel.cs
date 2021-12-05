using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class VideoFileViewModel : BaseViewModel
    {
        #region Navigation
        public VideoFileViewModel ThisVideoFileViewModel { get; set; }
        private FileOverviewViewModel FileOverviewViewModel { get; set; }
        private TracksViewModel TracksViewModel { get; set; }
        private AudioEditViewModel AudioEditViewModel { get; set; }
        public BaseViewModel CurrentVideoFileViewModel { get; set; }
        public ICommand UpdateCurrentVideoFileViewModelCommand { get; set; }
        #endregion

        private readonly IExternalLibrariesService _exLib;

        public Dictionary<ProcessResultNames, ProcessResult> ProcessResults { get; private set; } = new();

        #region I/O
        public string InputPath { get; private set; }
        public string InputName { get; private set; }
        public string InputExtension { get; private set; }
        public string InputFullName => InputName + InputExtension;
        public string InputFullPath { get; private set; }
        public string OutputPath { get; set; }
        public string OutputName { get; set; }
        public static string OutputExtension => ".mkv";
        public string OutputFullName => $"{OutputName}.{OutputExtension}";
        public string OutputFullPath => Path.Combine(OutputPath, OutputFullName);
        #endregion

        public VideoFileViewModel(string source, IExternalLibrariesService externalLibrariesService)
        {
            ThisVideoFileViewModel = this;

            InputPath = Path.GetDirectoryName(source);
            InputName = Path.GetFileNameWithoutExtension(source);
            InputExtension = Path.GetExtension(source);
            InputFullPath = source;
            _exLib = externalLibrariesService;
            OutputPath = InputPath;
            OutputName = InputName + " - edit";

            FileOverviewViewModel = new FileOverviewViewModel(this);
            AudioEditViewModel = new AudioEditViewModel(this, _exLib);

            CallMKVMergeJ();
        }

        private async void CallMKVMergeJ()
        {
            ProcessResult pr = await _exLib.Run(this, ProcessResultNames.MKVMergeJ);
            ProcessResults[ProcessResultNames.MKVMergeJ] = pr;
            MKVMergeJ result = JsonConvert.DeserializeObject<MKVMergeJ>(ProcessResults[ProcessResultNames.MKVMergeJ].StdOutput);
            TracksViewModel = new TracksViewModel(this, result, _exLib);

            UpdateCurrentVideoFileViewModelCommand = new UpdateCurrentVideoFileViewModelCommand(this, FileOverviewViewModel, TracksViewModel, AudioEditViewModel);
            UpdateCurrentVideoFileViewModelCommand.Execute(ViewModelTypes.Tracks);
        }
    }
}
