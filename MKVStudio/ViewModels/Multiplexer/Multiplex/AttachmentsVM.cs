using MKVStudio.Commands;
using MKVStudio.Models;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class AttachmentsVM : BaseViewModel
    {
        public ICommand AddAttachment { get;set; }
        public ICommand RemoveAllAttachments { get; set; }

        public AttachmentsVM(MKVMergeJ.Attachment[] attachments)
        {

        }
    }
}
