using MKVStudio.Commands;
using MKVStudio.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class TagsVM : BaseViewModel
    {
        public ObservableCollection<GlobalTagVM> GlobalTags { get; set; } = new();
        public ObservableCollection<TrackTagVM> TrackTags { get; set; } = new();
        public ICommand RemoveAllTags => new RemoveAllTagsCommand(this);
        public ICommand AddTag => new AddTagCommand(this);

        public TagsVM(MKVMergeJ.Global_Tag[] globalTags, MKVMergeJ.Track_Tag[] trackTags)
        {
            foreach (MKVMergeJ.Global_Tag globalTag in globalTags)
            {
                GlobalTags.Add(new GlobalTagVM(this, globalTag));
            }
            foreach (MKVMergeJ.Track_Tag trackTag in trackTags)
            {
                TrackTags.Add(new TrackTagVM(this, trackTag));
            }
        }
    }
}
