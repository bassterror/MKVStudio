namespace MKVStudio.Models;

public class MIMEType
{
    public string Type { get; set; }
    public string Extension { get; set; }

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
                Extension = ".jpg";
                break;
            case "image/bmp":
                Extension = ".bmp";
                break;
            case "image/png":
                Extension = ".png";
                break;
            case "image/gif":
                Extension = ".gif";
                break;
            case "font/otf":
                Extension = ".otf";
                break;
            case "font/ttf":
                Extension = ".ttf";
                break;
            case "font/collection":
                Extension = ".ttc";
                break;
            case "text/plain":
                Extension = ".txt";
                break;
            case "application/msword":
                Extension = ".doc";
                break;
            case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                Extension = ".docx";
                break;
            case "application/pdf":
                Extension = ".pdf";
                break;
            case "application/json":
                Extension = ".json";
                break;
            case "text/csv":
                Extension = ".csv";
                break;
            default:
                break;
        }
    }
}
