namespace MKVStudio.Models;

public class MIMEType
{
    public string Type { get; set; }
    public string Extensions { get; set; }

    public MIMEType(string type = null, string sourceExtension = null)
    {
        if (sourceExtension != null)
        {
            SetType(sourceExtension);
            SetExtensions();
            return;
        }
        Type = type;
        SetExtensions();
    }

    private void SetType(string sourceExtension)
    {
        switch (sourceExtension)
        {
            case ".jpg":
            case ".jpeg":
                Type = "image/jpeg";
                break;
            case ".bmp":
                Type = "image/bmp";
                break;
            case ".png":
                Type = "image/png";
                break;
            case ".gif":
                Type = "image/gif";
                break;
            case ".otf":
                Type = "font/otf";
                break;
            case ".ttf":
                Type = "font/ttf";
                break;
            case ".ttc":
                Type = "font/collection";
                break;
            case ".txt":
                Type = "text/plain";
                break;
            case ".doc":
                Type = "application/msword";
                break;
            case ".docx":
                Type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                break;
            case ".pdf":
                Type = "application/pdf";
                break;
            case ".json":
                Type = "application/json";
                break;
            case ".csv":
                Type = "text/csv";
                break;
            default:
                break;
        }
    }

    private void SetExtensions()
    {
        switch (Type)
        {
            case "image/jpeg":
                Extensions = "*.jpg;*.jpeg";
                break;
            case "image/bmp":
                Extensions = "*.bmp";
                break;
            case "image/png":
                Extensions = "*.png";
                break;
            case "image/gif":
                Extensions = "*.gif";
                break;
            case "font/otf":
                Extensions = "*.otf";
                break;
            case "font/ttf":
                Extensions = "*.ttf";
                break;
            case "font/collection":
                Extensions = "*.ttc";
                break;
            case "text/plain":
                Extensions = "*.txt";
                break;
            case "application/msword":
                Extensions = "*.doc";
                break;
            case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                Extensions = "*.docx";
                break;
            case "application/pdf":
                Extensions = "*.pdf";
                break;
            case "application/json":
                Extensions = "*.json";
                break;
            case "text/csv":
                Extensions = "*.csv";
                break;
            default:
                break;
        }
    }
}
