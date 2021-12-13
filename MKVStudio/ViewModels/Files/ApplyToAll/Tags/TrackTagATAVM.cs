using MKVStudio.Commands;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class TrackTagATAVM : BaseViewModel
    {
        private readonly TagsATAVM _tags;

        public int NumEntries { get; set; }
        public int TrackID { get; set; }
        public ICommand RemoveTag => new RemoveTagATACommand(_tags, null, this);

        public TrackTagATAVM(TagsATAVM tags)
        {
            _tags = tags;
        }
    }
}
