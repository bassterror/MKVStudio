using MKVStudio.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class TagsATAVM : BaseViewModel
    {
        public ObservableCollection<GlobalTagATAVM> GlobalTags { get; set; } = new();
        public ObservableCollection<TrackTagATAVM> TrackTags { get; set; } = new();
        public ICommand RemoveAllTags => new RemoveAllTagsATACommand(this);
        public ICommand AddTag => new AddTagATACommand(this);
    }
}
