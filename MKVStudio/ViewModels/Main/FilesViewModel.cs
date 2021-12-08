using MKVStudio.Commands;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class FilesViewModel : BaseViewModel
    {
        private readonly IExternalLibrariesService _exLib;

        public BaseViewModel CurrentFilesViewModel { get; set; }
        public ObservableCollection<VideoFileViewModel> Videos { get; set; } = new();
        public VideoFileViewModel SelectedVideo { get; set; }
        public ICommand AddVideosCommand => new AddVideosCommand(Videos, _exLib);
        public ICommand AddVideosFromFolderCommand => new AddVideosFromFolderCommand(Videos, _exLib);
        public ICommand RemoveVideoCommand => new RemoveVideoCommand(Videos);
        public ICommand ClearVideosCommand => new ClearVideosCommand(Videos);
        public ICommand ApplyToAllCommand => new ApplyToAllCommand(Videos, _exLib);

        public FilesViewModel(IExternalLibrariesService externalLibrariesService)
        {
            CurrentFilesViewModel = this;
            _exLib = externalLibrariesService;
        }
    }
}
