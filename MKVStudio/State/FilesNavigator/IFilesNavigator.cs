using MKVStudio.Data;
using MKVStudio.ViewModels.VideoFile;
using System.Windows.Input;

namespace MKVStudio.State.FilesNavigator
{
    public interface IFilesNavigator
    {
        BaseVideoFileViewModel CurrentVideoFileViewModel { get; set; }
        Video SelectedVideo { get; }
        ICommand AddVideosCommand { get; }
        ICommand AddVideosFromFolderCommand { get; }
        ICommand RemoveVideoCommand { get; }
        ICommand ClearVideosCommand { get; }
    }
}
