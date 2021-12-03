using MKVStudio.Models;
using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class VideoFileViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }
        private readonly VideoFile _selectedVideo;

        public VideoFileViewModel(INavigator navigator, VideoFile selectedVideo)
        {
            Navigator = navigator;
            _selectedVideo = selectedVideo;
        }
    }
}
