using MKVStudio.Models;
using MKVStudio.Services;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

    public MIMEType ContentType { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public int ID { get; set; }
    public string UID { get; set; }
    public int Size { get; set; }
    public string SizeConverted => ExLib.Util.ConvertBytes(Size, 2);
    public string TempPath { get; set; }
    public ImageSource TempImage { get; set; }

    public AttachmentVM(AttachmentsVM attachments, SourceFileVM sourceFile, IExternalLibrariesService exLib, MKVMergeJ.Attachment attachment = null)
    {
        Attachments = attachments;
        SourceFile = sourceFile;
        ExLib = exLib;

        if (attachment == null)
        {
            IsChecked = true;
            ContentType = ExLib.MIMETypes.AttachmentMIMETypes.First(m => m.Extension.Contains(SourceFile.InputExtension));
            Name = sourceFile.InputName;
            Size = (int)new FileInfo(SourceFile.InputFullPath).Length;
            return;
        }

        ContentType = new MIMEType(attachment.Content_type);
        Description = attachment.Description;
        Name = attachment.File_name;
        ID = attachment.Id;
        UID = attachment.Properties.UID;
        Size = attachment.Size;
        TempPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"\\temp\\{sourceFile.InputName}\\{Name}{ContentType.Extension}";
        ExtractTemp();
    }

    private async void ExtractTemp()
    {
        _ = await ExLib.Run(ProcessResultNames.MKVExtractAttachments, SourceFile, ID.ToString(), TempPath);
        LoadTempImage();
    }

    private void LoadTempImage()
    {
        using var stream = new FileStream(TempPath, FileMode.Open, FileAccess.Read);
        var bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.DecodePixelWidth = 200;
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.StreamSource = stream;
        bitmap.EndInit();
        bitmap.Freeze();
        TempImage = bitmap;
    }
}
