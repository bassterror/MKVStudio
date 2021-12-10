using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class TagsATAVM : BaseViewModel
    {
        public ObservableCollection<GlobalTagATAVM> GlobalTags { get; set; } = new();
        public ObservableCollection<TrackTagATAVM> TrackTags { get; set; } = new();
        public ICommand RemoveAllTags { get; set; }
        public ICommand RemoveAllGlobalTags { get; set; }
        public ICommand RemoveAllTrackTags { get; set; }
        public ICommand AddTag { get; set; }

    }
}
