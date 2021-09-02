using MKVStudio.Models;
using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class VideoFileViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }
        private readonly Video _selectedVideo;

        public VideoFileViewModel(INavigator navigator, Video selectedVideo)
        {
            Navigator = navigator;
            _selectedVideo = selectedVideo;
        }
    }
}
