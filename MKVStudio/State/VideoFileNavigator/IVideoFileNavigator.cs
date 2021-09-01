using MKVStudio.ViewModels.VideoFile;
using System.Windows.Input;

namespace MKVStudio.State.VideoFileNavigator
{
    public enum VideoFileViewModelType
    {
        General,
        MediaInfo,
        Convert
    }

    public interface IVideoFileNavigator
    {
        BaseVideoFileViewModel CurrentVideoFileViewModel { get; set; }
        ICommand UpdateCurrentVideoFileViewModelCommand { get; }
    }
}
