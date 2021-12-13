using MKVStudio.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class AttachmentsATAVM : BaseViewModel
    {
        public ObservableCollection<AttachmentATAVM> Attachments { get; set; } = new();
        public ICommand AddAttachment => new AddAttachmentATACommand(this);
        public ICommand RemoveAllAttachments => new RemoveAllAttachmentsATACommand(this);
    }
}
