using MKVStudio.Services;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace MKVStudio.Models;

[JsonObject(MemberSerialization.OptIn)]
public class LanguageCollections
{
    public IUtilitiesService Util { get; set; }

    public ObservableCollection<Language> AllLanguages { get; set; } = new();
    [JsonProperty]
    public ObservableCollection<Language> PreferedLanguages { get; set; } = new();

    public async void GetLanguageList()
    {
        ProcessResult pr = await Util.ExLib.RunProcess(ProcessResultNames.MKVMergeLangList);
        string[] lines = pr.StdOutput.Split("\r\n");
        for (int i = 2; i < lines.Length - 1; i++)
        {
            Match match = Regex.Match(lines[i], @"^(.+)\|(.+)\|(.+)\|(.+)$");
            Language language = new(match);
            AllLanguages.Add(language);
        }
    }
}
