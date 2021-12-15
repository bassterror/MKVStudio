using MKVStudio.Commands;
using MKVStudio.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class AttachmentsVM : BaseViewModel
    {
        public ObservableCollection<AttachmentVM> Attachments { get; set; } = new();
        public ICommand AddAttachment => new AddAttachmentCommand(this);
        public ICommand RemoveAllAttachments => new RemoveAllAttachmentsCommand(this);

        public AttachmentsVM(MKVMergeJ.Attachment[] attachments)
        {
            foreach (MKVMergeJ.Attachment attachment in attachments)
            {
                Attachments.Add(new AttachmentVM(this, attachment));
            }
        }
    }
}
