using MKVStudio.Commands;
using MKVStudio.Data;
using MKVStudio.ViewModels.Files;
using MKVStudio.ViewModels.VideoFile;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.State.FilesNavigator
{
    public class FilesNavigator : IFilesNavigator
    {
        private Video selectedVideo;

        public BaseVideoFileViewModel CurrentVideoFileViewModel { get; set; }
        public ObservableCollection<Video> Videos { get; set; } = new();
        public Video SelectedVideo
        {
            get
            {
                return selectedVideo;
            }
            set
            {
                selectedVideo = value;
                CreateVideoFileViewModel();
            }
        }
        public ICommand AddVideosCommand => new AddVideosCommand(Videos);
        public ICommand AddVideosFromFolderCommand => new AddVideosFromFolderCommand(Videos);
        public ICommand RemoveVideoCommand => new RemoveVideoCommand(Videos);
        public ICommand ClearVideosCommand => new ClearVideosCommand(Videos);

        public void CreateVideoFileViewModel()
        {
            CurrentVideoFileViewModel = new VideoFileViewModel(SelectedVideo);
        }
    }
}
