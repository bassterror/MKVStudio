using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class VideoFileVM : BaseViewModel
    {
        #region Navigation
        public VideoFileVM ThisVideoFileViewModel { get; set; }
        public TracksVM TracksViewModel { get; set; }
        private FileOverviewVM FileOverviewViewModel { get; set; }
        private AudioEditVM AudioEditViewModel { get; set; }
        private VideoEditVM VideoEditViewModel { get; set; }
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

        public VideoFileVM(string source, IExternalLibrariesService externalLibrariesService)
        {
            ThisVideoFileViewModel = this;

            InputPath = Path.GetDirectoryName(source);
            InputName = Path.GetFileNameWithoutExtension(source);
            InputExtension = Path.GetExtension(source);
            InputFullPath = source;
            _exLib = externalLibrariesService;
            OutputPath = InputPath;
            OutputName = InputName + " - edit";

            FileOverviewViewModel = new FileOverviewVM(this);
            AudioEditViewModel = new AudioEditVM(this, _exLib);
            VideoEditViewModel = new VideoEditVM(this, _exLib);

            CallMKVMergeJ();
        }

        private async void CallMKVMergeJ()
        {
            ProcessResult pr = await _exLib.Run(ProcessResultNames.MKVMergeJ, this);
            ProcessResults[ProcessResultNames.MKVMergeJ] = pr;
            MKVMergeJ result = JsonConvert.DeserializeObject<MKVMergeJ>(ProcessResults[ProcessResultNames.MKVMergeJ].StdOutput);
            TracksViewModel = new TracksVM(this, result, _exLib);

            UpdateCurrentVideoFileViewModelCommand = new UpdateCurrentVideoFileViewModelCommand(this, FileOverviewViewModel, TracksViewModel, AudioEditViewModel, VideoEditViewModel);
            UpdateCurrentVideoFileViewModelCommand.Execute(ViewModelTypes.Tracks);
        }
    }
}
