using MKVStudio.Services;

namespace MKVStudio.ViewModels
{
    public class VideoEditVM : BaseViewModel
    {
        public VideoFileVM SelectedVideo { get; set; }

        public VideoEditVM(VideoFileVM videoFileViewModel, IExternalLibrariesService externalLibrariesService)
        {
            SelectedVideo = videoFileViewModel;
        }
    }
}
