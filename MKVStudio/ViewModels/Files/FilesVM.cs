using MKVStudio.Commands;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class FilesVM : BaseViewModel
    {
        private readonly IExternalLibrariesService _exLib;

        public BaseViewModel ThisFilesVM { get; set; }
        public ObservableCollection<VideoFileVM> Videos { get; set; } = new();
        public VideoFileVM SelectedVideo { get; set; }
        public ICommand AddVideos => new AddVideosCommand(Videos, _exLib);
        public ICommand AddVideosFromFolder => new AddVideosFromFolderCommand(Videos, _exLib);
        public ICommand RemoveVideo => new RemoveVideoCommand(Videos);
        public ICommand ClearVideos => new ClearVideosCommand(Videos);
        public ICommand ApplyToAll => new ApplyToAllCommand(Videos, _exLib);

        public FilesVM(IExternalLibrariesService externalLibrariesService)
        {
            ThisFilesVM = this;
            _exLib = externalLibrariesService;
        }
    }
}
