using MKVStudio.ViewModels;
using System.Windows.Input;

namespace MKVStudio.State
{
    public enum VideoFileViewModelType
    {
        General,
        MediaInfo,
        Convert
    }

    public interface IVideoFileNavigator
    {
        BaseViewModel CurrentVideoFileViewModel { get; set; }
        ICommand UpdateCurrentVideoFileViewModelCommand { get; }
    }
}
