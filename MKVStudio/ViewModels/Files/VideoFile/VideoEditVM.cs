using MKVStudio.Services;

namespace MKVStudio.ViewModels
{
    public class VideoEditVM : BaseViewModel
    {
        public MultiplexVM SelectedVideo { get; set; }

        public VideoEditVM(MultiplexVM videoFileViewModel, IExternalLibrariesService externalLibrariesService)
        {
            SelectedVideo = videoFileViewModel;
        }
    }
}
