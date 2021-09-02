using MKVStudio.Models;
using MKVStudio.State;
using MKVStudio.State.VideoFileNavigator;

namespace MKVStudio.ViewModels
{
    public class VideoFileViewModel : BaseViewModel
    {
        public IVideoFileNavigator Navigator { get; set; }
        private readonly Video _selectedVideo;

        public VideoFileViewModel()
        {
            
            Navigator = new VideoFileNavigator();
        }
    }
}
