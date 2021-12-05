namespace MKVStudio.ViewModels
{
    public class AudioEditViewModel : BaseViewModel
    {
        public VideoFileViewModel SelectedVideo { get; set; }

        public AudioEditViewModel(VideoFileViewModel selectedVideo)
        {
            SelectedVideo = selectedVideo;
        }
    }
}
