using MKVStudio.Models;
using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class TracksViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }
        private readonly VideoFile _selectedVideo;

        public TracksViewModel(INavigator navigator, VideoFile selectedVideo)
        {
            Navigator = navigator;
            _selectedVideo = selectedVideo;
        }
    }
}
