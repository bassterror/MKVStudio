using MKVStudio.Models;
using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class GeneralViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }
        private readonly VideoFile _selectedVideo;

        public GeneralViewModel(INavigator navigator, VideoFile selectedVideo)
        {
            Navigator = navigator;
            _selectedVideo = selectedVideo;
        }
    }
}
