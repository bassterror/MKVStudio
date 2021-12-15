using MKVStudio.Commands;
using MKVStudio.Models;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class AttachmentVM
    {
        public string ID { get; set; }
        public string UID { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public ICommand RemoveAttachment { get; set; }

        public AttachmentVM(AttachmentsVM attachmentsVM, MKVMergeJ.Attachment attachment = null)
        {
            RemoveAttachment = new RemoveAttachmentCommand(attachmentsVM, this);
            if (attachment != null)
            {
                ID = attachment.Id.ToString();
                UID = attachment.Properties.UID;
                ContentType = attachment.Content_type;
                Name = attachment.File_name;
                Description = attachment.Description;
                Size = attachment.Size.ToString();
            }
        }
    }
}
