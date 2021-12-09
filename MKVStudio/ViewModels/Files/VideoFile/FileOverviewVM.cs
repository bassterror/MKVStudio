namespace MKVStudio.ViewModels
{
    public class FileOverviewVM : BaseViewModel
    {
        public VideoFileVM SelectedVideo { get; set; }

        public FileOverviewVM(VideoFileVM selectedVideo)
        {
            SelectedVideo = selectedVideo;
        }
    }
}
