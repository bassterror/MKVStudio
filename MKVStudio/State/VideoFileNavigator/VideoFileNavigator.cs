using MKVStudio.Commands;
using MKVStudio.ViewModels.VideoFile;
using System.Windows.Input;

namespace MKVStudio.State.VideoFileNavigator
{
    public class VideoFileNavigator : IVideoFileNavigator
    {
        public BaseVideoFileViewModel CurrentVideoFileViewModel { get; set; }

        public ICommand UpdateCurrentVideoFileViewModelCommand => new UpdateCurrentVideoFileViewModelCommand(this);
    }
}
