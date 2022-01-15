using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class AttachmentsVM : BaseViewModel
{
    public IUtilitiesService Util { get; }

    #region existing
    public ObservableCollection<Attachment> ExistingAttachments { get; set; } = new();
    public Attachment SelectedExistingAttachment { get; set; }
    public bool IsCheckAllEnabled { get; set; }
    public bool IsUncheckAllEnabled { get; set; }
    public ICommand CheckAll => new CheckBoxCommand(ExistingAttachments);
    #endregion

    #region new
    public ObservableCollection<Attachment> NewAttachments { get; set; } = new();
    public Attachment SelectedNewAttachment { get; set; }
    public bool IsSettingsEnabled => SelectedNewAttachment != null;
    public ICommand AddAttachment => new AddFilesCommand(this, Util);
    public ICommand RemoveAttachment => new RemoveFilesCommand(this);
    public ICommand ClearAttachments => new ClearFilesCommand(this);
    #endregion

    public AttachmentsVM(IUtilitiesService util, SourceFileVM sourceFile, MKVMergeJ.Attachment[] attachments)
    {
        Util = util;
        if (attachments != null)
        {
            foreach (MKVMergeJ.Attachment attachment in attachments)
            {
                Attachment att = new(Util, sourceFile, this, attachment);
                ExistingAttachments.Add(att);
            }
            IsCheckAllEnabled = ExistingAttachments.Count(a => a.ToBeAdded) != ExistingAttachments.Count;
            IsUncheckAllEnabled = ExistingAttachments.Count(a => !a.ToBeAdded) != ExistingAttachments.Count;
        }
    }
}
