using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.ViewModels;
using MKVStudio.ViewModels.Factories;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.State
{
    public class FilesNavigator : BaseNavigator, IFilesNavigator
    {
        public BaseViewModel CurrentViewModel { get; set; }
        public ObservableCollection<Video> Videos { get; set; } = new();
        public Video SelectedVideo { get; set; }

        public ICommand AddVideosCommand => new AddVideosCommand(Videos);
        public ICommand AddVideosFromFolderCommand => new AddVideosFromFolderCommand(Videos);
        public ICommand RemoveVideoCommand => new RemoveVideoCommand(Videos);
        public ICommand ClearVideosCommand => new ClearVideosCommand(Videos);
        public ICommand UpdateCurrentViewModel { get; set; }

        public FilesNavigator()
        {
            //UpdateCurrentViewModel = new UpdateCurrentFilesViewModelCommand(this, viewModelFactory, SelectedVideo);
        }
    }
}
