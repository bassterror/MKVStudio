using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using MKVStudio.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.State
{
    public class Navigator : BaseNavigator, INavigator
    {
        private Video _selectedVideo;
        private readonly IExternalLibrariesService _exLib;

        public BaseViewModel CurrentMainViewModel { get; set; }
        public BaseViewModel CurrentFilesViewModel { get; set; }
        public BaseViewModel CurrentVideoFileViewModel { get; set; }
        private FilesViewModel FilesViewModel { get; set; }
        private GeneralViewModel GeneralViewModel { get; set; }
        private MediaInfoViewModel MediaInfoViewModel { get; set; }
        private ConvertViewModel ConvertViewModel { get; set; }
        private QueueViewModel QueueViewModel { get; set; }
        public ICommand UpdateCurrentMainViewModelCommand { get; set; }
        public ICommand UpdateCurrentFilesViewModelCommand { get; set; }
        public ICommand UpdateCurrentVideoFileViewModelCommand { get; set; }
        public ObservableCollection<Video> Videos { get; set; } = new();
        public Video SelectedVideo
        {
            get => _selectedVideo;
            set
            {
                _selectedVideo = value;
                UpdateCurrentFilesViewModelCommand = new UpdateCurrentFilesViewModelCommand(this, SelectedVideo);
                UpdateCurrentFilesViewModelCommand.Execute(ViewModelTypes.VideoFile);
                GeneralViewModel = new GeneralViewModel(this, SelectedVideo);
                MediaInfoViewModel = new MediaInfoViewModel(this, SelectedVideo);
                ConvertViewModel = new ConvertViewModel(this, SelectedVideo);
                UpdateCurrentVideoFileViewModelCommand = new UpdateCurrentVideoFileViewModelCommand(this, GeneralViewModel, MediaInfoViewModel, ConvertViewModel);
                UpdateCurrentVideoFileViewModelCommand.Execute(ViewModelTypes.General);
            }
        }
        public ICommand AddVideosCommand => new AddVideosCommand(Videos, _exLib);
        public ICommand AddVideosFromFolderCommand => new AddVideosFromFolderCommand(Videos, _exLib);
        public ICommand RemoveVideoCommand => new RemoveVideoCommand(Videos);
        public ICommand ClearVideosCommand => new ClearVideosCommand(Videos);

        public Navigator(IExternalLibrariesService externalLibrariesService)
        {
            FilesViewModel = new FilesViewModel(this);
            QueueViewModel = new QueueViewModel(this);
            UpdateCurrentMainViewModelCommand = new UpdateCurrentMainViewModelCommand(this, FilesViewModel, QueueViewModel);
            _exLib = externalLibrariesService;
        }
    }
}
