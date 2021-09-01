using MKVStudio.Data;
using MKVStudio.State.VideoFileNavigator;

namespace MKVStudio.ViewModels.VideoFile
{
    public class VideoFileViewModel : BaseVideoFileViewModel
    {
        public IVideoFileNavigator Navigator { get; set; } = new VideoFileNavigator();
        private readonly Video _selectedVideo;

        public VideoFileViewModel(Video selectedVideo)
        {
            _selectedVideo = selectedVideo;
        }
    }
}
