using System.Collections.Generic;

namespace MKVStudio.Models;

public class MIMETypeCollections
{
    public List<MIMEType> AttachmentMIMETypes { get; set; } = new();

    public MIMETypeCollections()
    {
        AttachmentMIMETypes.Add(new MIMEType("image/jpeg"));
        AttachmentMIMETypes.Add(new MIMEType("image/bmp"));
        AttachmentMIMETypes.Add(new MIMEType("image/png"));
        AttachmentMIMETypes.Add(new MIMEType("image/gif"));
        AttachmentMIMETypes.Add(new MIMEType("font/otf"));
        AttachmentMIMETypes.Add(new MIMEType("font/ttf"));
        AttachmentMIMETypes.Add(new MIMEType("font/collection"));
        AttachmentMIMETypes.Add(new MIMEType("text/plain"));
        AttachmentMIMETypes.Add(new MIMEType("application/msword"));
        AttachmentMIMETypes.Add(new MIMEType("application/vnd.openxmlformats-officedocument.wordprocessingml.document"));
        AttachmentMIMETypes.Add(new MIMEType("application/pdf"));
        AttachmentMIMETypes.Add(new MIMEType("application/json"));
        AttachmentMIMETypes.Add(new MIMEType("text/csv"));
    }
}
