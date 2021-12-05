﻿using MKVStudio.Models;
using MKVStudio.Services;

namespace MKVStudio.ViewModels
{
    public class SubtitleTrackViewModel : BaseViewModel
    {
        public VideoFileViewModel SelectedVideo { get; }

        public string Name { get; set; }
        public string ID { get; set; }
        public string UID { get; set; }
        public string Codec { get; set; }
        public string CodecId { get; set; }
        public bool DefaultTrack { get; set; }
        public bool EnabledTrack { get; set; }
        public bool ForcedTrack { get; set; }
        public bool FlagHearingImpaired { get; set; }
        public bool FlagCommentary { get; set; }
        public string Encoding { get; set; }
        public Language Language { get; set; }
        public string LanguageIETF { get; set; }
        public int Number { get; set; }
        public string ContentEncodingAlgorithms { get; set; }

        public SubtitleTrackViewModel(VideoFileViewModel videoFileViewModel, MKVMergeJ.Track track, IExternalLibrariesService externalLibrariesService)
        {
            SelectedVideo = videoFileViewModel;
            Name = track.Properties.Track_name;
            ID = track.ID.ToString();
            UID = track.Properties.UID;
            Codec = track.Codec;
            CodecId = track.Properties.Codec_id;
            DefaultTrack = track.Properties.Default_track;
            EnabledTrack = track.Properties.Enabled_track;
            ForcedTrack = track.Properties.Forced_track;
            FlagHearingImpaired = track.Properties.Flag_hearing_impaired;
            FlagCommentary = track.Properties.Flag_commentary;
            Language = string.IsNullOrWhiteSpace(track.Properties.Language) ? externalLibrariesService.Languages["und"] : externalLibrariesService.Languages[track.Properties.Language];
            LanguageIETF = track.Properties.Language_ietf;
            Number = track.Properties.Number;
            ContentEncodingAlgorithms = track.Properties.Content_encoding_algorithms;
        }
    }
}
