using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.ViewModels;
using System.Windows.Input;

namespace MKVStudio.State.VideoFileNavigator
{
    public class VideoFileNavigator : IVideoFileNavigator
    {
        public BaseViewModel CurrentVideoFileViewModel { get; set; }
        public Video SelectedVideo { get; set; }
        public ICommand UpdateCurrentVideoFileViewModelCommand => new UpdateCurrentVideoFileViewModelCommand(this, SelectedVideo.BuildArguments("firstPass"), "firstPass");

        public VideoFileNavigator(Video selectedVideo)
        {
            SelectedVideo = selectedVideo;
        }
    }
}
