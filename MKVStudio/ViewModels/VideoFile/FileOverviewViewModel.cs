namespace MKVStudio.ViewModels
{
    public class FileOverviewViewModel : BaseViewModel
    {
        public VideoFileViewModel SelectedVideo { get; set; }

        public FileOverviewViewModel(VideoFileViewModel selectedVideo)
        {
            SelectedVideo = selectedVideo;
        }
    }
}
