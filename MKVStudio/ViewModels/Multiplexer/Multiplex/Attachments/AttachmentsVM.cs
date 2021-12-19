using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class AttachmentsVM : BaseViewModel
    {
        public SourceFileVM SourceFile { get; }
        public IExternalLibrariesService ExLib { get; }

        #region existing
        public ObservableCollection<AttachmentVM> ExistingAttachments { get; set; } = new();
        public AttachmentVM SelectedExistingAttachment { get; set; }
        public bool IsCheckAll { get; set; }
        public bool IsUncheckAll { get; set; }
        public ICommand CheckAll => new CheckBoxCommand(ExistingAttachments);
        #endregion

        #region new
        public ObservableCollection<AttachmentVM> NewAttachments { get; set; } = new();
        public AttachmentVM SelectedNewAttachment { get; set; }
        public ICommand AddAttachment { get; set; }
        public ICommand RemoveAttachment { get; set; }
        public ICommand RemoveAllAttachments { get; set; }
        #endregion

        public AttachmentsVM(SourceFileVM sourceFile, MKVMergeJ.Attachment[] attachments, IExternalLibrariesService exLib)
        {
            SourceFile = sourceFile;
            ExLib = exLib;
            if (attachments != null)
            {
                foreach (MKVMergeJ.Attachment attachment in attachments)
                {
                    AttachmentVM att = new(this, SourceFile, attachment, ExLib);
                    ExistingAttachments.Add(att);
                    att.IsChecked = true;
                }
            }
        }
    }
}
