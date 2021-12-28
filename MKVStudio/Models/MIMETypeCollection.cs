using System.Collections.Generic;

namespace MKVStudio.Models;

public class MIMETypeCollection
{
    public List<MIMEType> AttachmentMIMETypes { get; set; } = new();

    public MIMETypeCollection()
    {
        AttachmentMIMETypes.Add(new MIMEType("image/jpeg", "*.jpg;*.jpeg"));
        AttachmentMIMETypes.Add(new MIMEType("image/bmp", "*.bmp"));
        AttachmentMIMETypes.Add(new MIMEType("image/png", "*.png"));
        AttachmentMIMETypes.Add(new MIMEType("image/gif", "*.gif"));
        AttachmentMIMETypes.Add(new MIMEType("font/otf", "*.otf"));
        AttachmentMIMETypes.Add(new MIMEType("font/ttf", "*.ttf"));
        AttachmentMIMETypes.Add(new MIMEType("font/collection", "*.ttc"));
        AttachmentMIMETypes.Add(new MIMEType("text/plain", "*.txt"));
        AttachmentMIMETypes.Add(new MIMEType("application/msword", "*.doc"));
        AttachmentMIMETypes.Add(new MIMEType("application/vnd.openxmlformats-officedocument.wordprocessingml.document", " *.docx"));
        AttachmentMIMETypes.Add(new MIMEType("application/pdf", "*.pdf"));
        AttachmentMIMETypes.Add(new MIMEType("application/json", "*.json"));
        AttachmentMIMETypes.Add(new MIMEType("text/csv", "*.csv"));
    }
}
