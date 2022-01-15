using MKVStudio.Services;
using MKVStudio.ViewModels;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MKVStudio.Models;

public class Attachment : BaseModel
{
    private readonly string _tempPath;
    private readonly AttachmentsVM _attachments;
    private bool _toBeAdded;

    public IUtilitiesService Util { get; }
    public SourceFileInfo SourceFile { get; }
    public bool ToBeAdded
    {
        get => _toBeAdded;
        set
        {
            _toBeAdded = value;
            _attachments.IsCheckAllEnabled = _attachments.ExistingAttachments.Count(a => a.ToBeAdded) != _attachments.ExistingAttachments.Count;
            _attachments.IsUncheckAllEnabled = _attachments.ExistingAttachments.Count(a => !a.ToBeAdded) != _attachments.ExistingAttachments.Count;
        }
    }
    public MIMEType ContentType { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public int ID { get; set; }
    public string UID { get; set; }
    public int Size { get; set; }
    public string SizeConverted => Util.ConvertBytes(Size, 2);
    public ImageSource TempImage { get; set; }

    public Attachment(IUtilitiesService util, SourceFileInfo sourceFile, AttachmentsVM attachments, MKVMergeJ.Attachment attachment = null)
    {
        Util = util;
        SourceFile = sourceFile;
        _attachments = attachments;
        ToBeAdded = true;

        if (attachment == null)
        {
            ContentType = Util.MIMETypes.AttachmentMIMETypes.First(m => m.Extension.Contains(SourceFile.InputExtension));
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
        _tempPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"\\temp\\{sourceFile.InputName}\\{Name}{ContentType.Extension}";
        ExtractTemp();
    }

    private async void ExtractTemp()
    {
        _ = await Util.ExLib.Run(ProcessResultNames.MKVExtractAttachments, SourceFile, ID.ToString(), _tempPath);
        LoadTempImage();
    }

    private void LoadTempImage()
    {
        using FileStream stream = new(_tempPath, FileMode.Open, FileAccess.Read);
        BitmapImage bitmap = new();
        bitmap.BeginInit();
        bitmap.DecodePixelWidth = 200;
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.StreamSource = stream;
        bitmap.EndInit();
        bitmap.Freeze();
        TempImage = bitmap;
    }
}
