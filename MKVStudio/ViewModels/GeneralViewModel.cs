using MKVStudio.Models;
using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class GeneralViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }
        private readonly Video _selectedVideo;

        public GeneralViewModel(INavigator navigator, Video selectedVideo)
        {
            Navigator = navigator;
            _selectedVideo = selectedVideo;
        }
    }
}
