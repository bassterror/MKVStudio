namespace MKVStudio.Models;

public class MIMEType
{
    public string Type { get; set; }
    public string Extensions { get; set; }

    public MIMEType(string type, string extensions)
    {
        Type = type;
        Extensions = extensions;
    }
}
