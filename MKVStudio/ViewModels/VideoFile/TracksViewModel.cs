namespace MKVStudio.ViewModels
{
    public class TracksViewModel : BaseViewModel
    {
        public VideoFileViewModel SelectedVideo { get; set; }

        public TracksViewModel(VideoFileViewModel selectedVideo)
        {
            SelectedVideo = selectedVideo;
        }
    }
}
