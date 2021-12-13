using MKVStudio.Commands;
using MKVStudio.Models;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class TrackTagVM : BaseViewModel
    {
        public int NumEntries { get; set; }
        public int TrackID { get; set; }
        public ICommand RemoveTag { get; set; }

        public TrackTagVM(TagsVM tags, MKVMergeJ.Track_Tag trackTag = null)
        {
            RemoveTag = new RemoveTagCommand(tags, null, this);
            if (trackTag != null)
            {
                NumEntries = trackTag.Num_entries;
                TrackID = trackTag.Track_id;
            }
        }
    }
}
