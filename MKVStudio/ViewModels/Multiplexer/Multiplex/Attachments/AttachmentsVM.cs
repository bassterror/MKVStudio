using MKVStudio.Models;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class AttachmentsVM : BaseViewModel
    {
        public SourceFileVM SourceFile { get; }
        public IExternalLibrariesService ExLib { get; }
        public ObservableCollection<AttachmentVM> ExistingAttachments { get; set; } = new();
        public AttachmentVM SelectedExistingAttachment { get; set; }
        public bool AreAllExistingChecked
        {
            get => ExistingAttachments.Count == ExistingAttachments.Count(a => a.IsChecked);
            set
            {
                foreach (AttachmentVM attachment in ExistingAttachments)
                {
                    attachment.IsChecked = value;
                }
            }
        }

        public ObservableCollection<AttachmentVM> NewAttachments { get; set; } = new();
        public AttachmentVM SelectedNewAttachment { get; set; }
        public bool AreAllNewChecked
        {
            get => NewAttachments.Count == NewAttachments.Count(a => a.IsChecked);
            set
            {
                foreach (AttachmentVM attachment in NewAttachments)
                {
                    attachment.IsChecked = value;
                }
            }
        }

        public ICommand AddAttachment { get; set; }
        public ICommand RemoveAttachment { get; set; }
        public ICommand RemoveAllAttachments { get; set; }

        public AttachmentsVM(SourceFileVM sourceFile, MKVMergeJ.Attachment[] attachments, IExternalLibrariesService exLib)
        {
            SourceFile = sourceFile;
            ExLib = exLib;
            if (attachments != null)
            {
                foreach (MKVMergeJ.Attachment attachment in attachments)
                {
                    ExistingAttachments.Add(new AttachmentVM(this, attachment, ExLib));
                }
            }
        }
    }
}
