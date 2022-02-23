﻿using MKVStudio.Services;
using System.Linq;

namespace MKVStudio.Models;

public class AudioProperties : BaseModel
{
    public string Name { get; set; }
    public string ID { get; set; }
    public string UID { get; set; }
    public string Codec { get; set; }
    public string CodecId { get; set; }
    public bool DefaultTrack { get; set; }
    public bool EnabledTrack { get; set; }
    public bool ForcedTrack { get; set; }
    public bool FlagVisualImpaired { get; set; }
    public bool FlagOriginal { get; set; }
    public bool FlagCommentary { get; set; }
    public int Channels { get; set; }
    public int SampleRate { get; set; }
    public Language Language { get; set; }
    public string LanguageIETF { get; set; }
    public int Number { get; set; }

    public AudioProperties(IUtilitiesService util, MKVMergeJ.Track track)
    {
        Name = track.Properties.Track_name;
        ID = track.ID.ToString();
        UID = track.Properties.UID;
        Codec = track.Codec;
        CodecId = track.Properties.Codec_id;
        DefaultTrack = track.Properties.Default_track;
        EnabledTrack = track.Properties.Enabled_track;
        ForcedTrack = track.Properties.Forced_track;
        FlagVisualImpaired = track.Properties.Flag_visual_impaired;
        FlagOriginal = track.Properties.Flag_original;
        FlagCommentary = track.Properties.Flag_commentary;
        Channels = track.Properties.Audio_channels;
        SampleRate = track.Properties.Audio_sampling_frequency;
        Language = string.IsNullOrWhiteSpace(track.Properties.Language) ?
            util.Preferences.Languages.AllLanguages.First(a => a.ID == "und") :
            util.Preferences.Languages.AllLanguages.First(a => a.ID == track.Properties.Language);
        LanguageIETF = track.Properties.Language_ietf;
        Number = track.Properties.Number;
    }
}
