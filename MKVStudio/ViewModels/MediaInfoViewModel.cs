using MKVStudio.Models;
using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class MediaInfoViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }
        private readonly Video _selectedVideo;

        public MediaInfoViewModel(INavigator navigator, Video selectedVideo)
        {
            Navigator = navigator;
            _selectedVideo = selectedVideo;
        }
    }
}
