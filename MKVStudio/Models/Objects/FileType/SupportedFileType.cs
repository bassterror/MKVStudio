using System.Text.RegularExpressions;

namespace MKVStudio.Models;

public class SupportedFileType
{
    public string Name { get; set; }
    public string Extensions { get; set; }

    public SupportedFileType(Match match)
    {
        Name = match.Groups[1].ToString();
        string[] exts = match.Groups[2].ToString().Split(" ");
        foreach (string ext in exts)
        {
            Extensions += $"*.{ext};";
        }
        Extensions = Extensions.Remove(Extensions.Length - 1, 1);
    }

    public SupportedFileType(string name, string extensions)
    {
        Name = name;
        Extensions = extensions;
    }
}
