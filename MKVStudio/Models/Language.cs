using System.Text.RegularExpressions;

namespace MKVStudio.Models;

public class Language
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string ISO6392 { get; set; }
    public string ISO6391 { get; set; }

    public Language(Match match)
    {
        ID = match.Groups[2].ToString().Trim();
        Name = match.Groups[1].ToString().Trim();
        ISO6392 = match.Groups[3].ToString().Trim();
        ISO6391 = match.Groups[4].ToString().Trim();
    }
}
