namespace MKVStudio.ViewModels
{
    public class VideoEditViewModel : BaseViewModel
    {
        public VideoFileViewModel SelectedVideo { get; set; }

        public VideoEditViewModel(VideoFileViewModel videoFileViewModel)
        {
            SelectedVideo = videoFileViewModel;
        }
    }
}
