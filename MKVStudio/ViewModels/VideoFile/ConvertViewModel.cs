namespace MKVStudio.ViewModels
{
    public class ConvertViewModel : BaseViewModel
    {
        public VideoFileViewModel SelectedVideo { get; set; }

        public ConvertViewModel(VideoFileViewModel selectedVideo)
        {
            SelectedVideo = selectedVideo;
        }
    }
}
