using MKVStudio.Services;
using Newtonsoft.Json;
using System.IO;

namespace MKVStudio.Models;

[JsonObject(MemberSerialization.OptIn)]
public class Preferences
{
    [JsonProperty]
    public string FfmpegPath { get; set; }
    [JsonProperty]
    public string MkvInfoPath { get; set; }
    [JsonProperty]
    public string MkvMergePath { get; set; }
    [JsonProperty]
    public string MkvPropEditPath { get; set; }
    [JsonProperty]
    public string MkvExtractPath { get; set; }

    [JsonProperty]
    public LanguageCollections Languages { get; set; }
    public SupportedFileTypesCollections SupportedFileTypes { get; set; }
    public MIMETypeCollections MIMETypes { get; set; }
    [JsonProperty]
    public OutputName OutputName { get; set; }

    public static Preferences GetPreferences(IUtilitiesService util, string fileName)
    {
        Executable ffmpeg;
        Executable mkvInfo;
        Executable mkvMerge;
        Executable mkvPropEdit;
        Executable mkvExtract;

        Preferences preferences = Read(fileName);
        if (string.IsNullOrWhiteSpace(preferences.FfmpegPath))
        {
            ffmpeg = new(ExecutableNames.FFmpeg, util.GetFileDialog("ffMPEG|ffmpeg.exe").FileName);
            util.ExLib.Executables[ExecutableNames.FFmpeg] = ffmpeg;
            preferences.FfmpegPath = ffmpeg.Path;
            preferences.Save(fileName);
        }
        if (string.IsNullOrWhiteSpace(preferences.MkvInfoPath))
        {
            mkvInfo = new(ExecutableNames.MKVInfo, util.GetFileDialog("MKV Info|mkvinfo.exe").FileName);
            util.ExLib.Executables[ExecutableNames.MKVInfo] = mkvInfo;
            preferences.MkvInfoPath = mkvInfo.Path;
            preferences.Save(fileName);
        }
        if (string.IsNullOrWhiteSpace(preferences.MkvMergePath))
        {
            mkvMerge = new(ExecutableNames.MKVMerge, util.GetFileDialog("MKV Merge|mkvmerge.exe").FileName);
            util.ExLib.Executables[ExecutableNames.MKVMerge] = mkvMerge;
            preferences.MkvMergePath = mkvMerge.Path;
            preferences.Save(fileName);
        }
        if (string.IsNullOrWhiteSpace(preferences.MkvPropEditPath))
        {
            mkvPropEdit = new(ExecutableNames.MKVPropEdit, util.GetFileDialog("MKV Prop Edit|mkvpropedit.exe").FileName);
            util.ExLib.Executables[ExecutableNames.MKVPropEdit] = mkvPropEdit;
            preferences.MkvPropEditPath = mkvPropEdit.Path;
            preferences.Save(fileName);
        }
        if (string.IsNullOrWhiteSpace(preferences.MkvExtractPath))
        {
            mkvExtract = new(ExecutableNames.MKVExtract, util.GetFileDialog("MKV Extract|mkvextract.exe").FileName);
            util.ExLib.Executables[ExecutableNames.MKVExtract] = mkvExtract;
            preferences.MkvExtractPath = mkvExtract.Path;
            preferences.Save(fileName);
        }
        ffmpeg = new(ExecutableNames.FFmpeg, preferences.FfmpegPath);
        util.ExLib.Executables[ExecutableNames.FFmpeg] = ffmpeg;
        mkvInfo = new(ExecutableNames.MKVInfo, preferences.MkvInfoPath);
        util.ExLib.Executables[ExecutableNames.MKVInfo] = mkvInfo;
        mkvMerge = new(ExecutableNames.MKVMerge, preferences.MkvMergePath);
        util.ExLib.Executables[ExecutableNames.MKVMerge] = mkvMerge;
        mkvPropEdit = new(ExecutableNames.MKVPropEdit, preferences.MkvPropEditPath);
        util.ExLib.Executables[ExecutableNames.MKVPropEdit] = mkvPropEdit;
        mkvExtract = new(ExecutableNames.MKVExtract, preferences.MkvExtractPath);
        util.ExLib.Executables[ExecutableNames.MKVExtract] = mkvExtract;
        if (preferences.Languages == null)
        {
            preferences.Languages = new();
        }
        preferences.Languages.Util = util;
        preferences.Languages.GetLanguageList();
        preferences.SupportedFileTypes = new(util);
        preferences.MIMETypes = new();
        if (preferences.OutputName == null)
        {
            preferences.OutputName = new();
        }
        preferences.OutputName.Util = util;

        return preferences;
    }

    public void Save(string fileName)
    {
        JsonSerializer serializer = new();
        serializer.NullValueHandling = NullValueHandling.Ignore;
        serializer.ObjectCreationHandling = ObjectCreationHandling.Replace;
        serializer.Formatting = Formatting.Indented;
        using StreamWriter sw = new(fileName);
        using JsonWriter jsonWriter = new JsonTextWriter(sw);
        serializer.Serialize(jsonWriter, this);
    }

    private static Preferences Read(string filename)
    {
        string json = File.ReadAllText(filename);
        Preferences preferences = JsonConvert.DeserializeObject<Preferences>(json);
        return preferences ?? new Preferences();
    }
}
