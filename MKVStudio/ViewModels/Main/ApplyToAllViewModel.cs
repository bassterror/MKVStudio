using System.Collections.ObjectModel;

namespace MKVStudio.ViewModels
{
    public class ApplyToAllViewModel
    {
        public ObservableCollection<VideoFileViewModel> Videos { get; set; }

        public ApplyToAllViewModel(ObservableCollection<VideoFileViewModel> videos)
        {
            Videos = videos;
        }
    }
}
