using MKVStudio.Commands;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class AttachmentATAVM : BaseViewModel
    {
        private readonly AttachmentsATAVM _attachments;

        public string ContentType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICommand RemoveAttachment => new RemoveAttachmentATACommand(_attachments, this);

        public AttachmentATAVM(AttachmentsATAVM attachments)
        {
            _attachments = attachments;
        }
    }
}
