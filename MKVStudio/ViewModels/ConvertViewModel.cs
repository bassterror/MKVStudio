using MKVStudio.Models;
using MKVStudio.State;

namespace MKVStudio.ViewModels
{
    public class ConvertViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }
        private readonly Video _selectedVideo;

        public ConvertViewModel(INavigator navigator, Video selectedVideo)
        {
            Navigator = navigator;
            _selectedVideo = selectedVideo;
        }
    }
}
