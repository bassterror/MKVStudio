using MKVStudio.Models;
using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class ConvertViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }
        private readonly VideoFile _selectedVideo;

        public ConvertViewModel(INavigator navigator, VideoFile selectedVideo)
        {
            Navigator = navigator;
            _selectedVideo = selectedVideo;
        }
    }
}
