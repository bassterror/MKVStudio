using MKVStudio.Services;

namespace MKVStudio.ViewModels
{
    public class VideoEditViewModel : BaseViewModel
    {
        public VideoFileViewModel SelectedVideo { get; set; }

        public VideoEditViewModel(VideoFileViewModel videoFileViewModel, IExternalLibrariesService externalLibrariesService)
        {
            SelectedVideo = videoFileViewModel;
        }
    }
}
