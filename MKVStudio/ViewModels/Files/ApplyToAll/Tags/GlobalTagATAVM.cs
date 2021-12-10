using MKVStudio.Commands;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class GlobalTagATAVM : BaseViewModel
    {
        private readonly TagsATAVM _tags;

        public int NumEntries { get; set; }
        public ICommand RemoveTag => new RemoveTagATACommand(_tags, this, null);

        public GlobalTagATAVM(TagsATAVM tags)
        {
            _tags = tags;
        }
    }
}
