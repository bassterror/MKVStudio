using MKVStudio.Models;
using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class MediaInfoViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }
        private readonly VideoFile _selectedVideo;

        public MediaInfoViewModel(INavigator navigator, VideoFile selectedVideo)
        {
            Navigator = navigator;
            _selectedVideo = selectedVideo;
        }
    }
}
