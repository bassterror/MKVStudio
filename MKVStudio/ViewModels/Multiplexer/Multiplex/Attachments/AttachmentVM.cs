using MKVStudio.Models;

namespace MKVStudio.ViewModels
{
    public class AttachmentVM : BaseViewModel
    {
        public AttachmentsVM Attachments { get; }
        public bool IsChecked { get; set; }

        public string ContentType { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public string UID { get; set; }
        public int Size { get; set; }

        public AttachmentVM(AttachmentsVM attachments, MKVMergeJ.Attachment attachment)
        {
            Attachments = attachments;

            IsChecked = true;
            ContentType = attachment.Content_type;
            Description = attachment.Description;
            Name = attachment.File_name;
            ID = attachment.Id;
            UID = attachment.Properties.UID;
            Size = attachment.Size;
        }
    }
}
