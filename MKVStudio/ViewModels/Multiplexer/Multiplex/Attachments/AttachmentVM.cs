using MKVStudio.Models;
using MKVStudio.Services;
using System.Linq;

namespace MKVStudio.ViewModels;

public class AttachmentVM : BaseViewModel
{
    public AttachmentsVM Attachments { get; }
    public SourceFileVM SourceFile { get; }
    public IExternalLibrariesService ExLib { get; }
    private bool _isChecked;
    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            _isChecked = value;
            Attachments.IsCheckAll = Attachments.ExistingAttachments.Count(a => a.IsChecked) != Attachments.ExistingAttachments.Count;
            Attachments.IsUncheckAll = Attachments.ExistingAttachments.Count(a => !a.IsChecked) != Attachments.ExistingAttachments.Count;
        }
    }

    public string ContentType { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public int ID { get; set; }
    public string UID { get; set; }
    public int Size { get; set; }
    public string SizeConverted => ExLib.Util.ConvertBytes(Size, 2);

    public AttachmentVM(AttachmentsVM attachments, SourceFileVM sourceFile, IExternalLibrariesService exLib, MKVMergeJ.Attachment attachment = null)
    {
        Attachments = attachments;
        SourceFile = sourceFile;
        ExLib = exLib;

        if (attachment != null)
        {
            ContentType = attachment.Content_type;
            Description = attachment.Description;
            Name = attachment.File_name;
            ID = attachment.Id;
            UID = attachment.Properties.UID;
            Size = attachment.Size;
        }
    }
}
