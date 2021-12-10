using MKVStudio.Commands;
using MKVStudio.Models;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class GlobalTagVM : BaseViewModel
    {
        public int NumEntries { get; set; }
        public ICommand RemoveTag { get; set; }

        public GlobalTagVM(TagsVM tags, MKVMergeJ.Global_Tag globalTag = null)
        {
            RemoveTag = new RemoveTagCommand(tags, this, null);
            if (globalTag != null)
            {
                NumEntries = globalTag.Num_entries;
            }
        }
    }
}
