using MKVStudio.Models;
using MKVStudio.ViewModels;
using System.Windows.Input;

namespace MKVStudio.State
{
    public interface IFilesNavigator
    {
        BaseViewModel CurrentViewModel { get; set; }
        Video SelectedVideo { get; }
        ICommand AddVideosCommand { get; }
        ICommand AddVideosFromFolderCommand { get; }
        ICommand RemoveVideoCommand { get; }
        ICommand ClearVideosCommand { get; }
    }
}
