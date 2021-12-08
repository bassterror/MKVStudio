﻿using MKVStudio.Models;
using MKVStudio.Services;

namespace MKVStudio.ViewModels
{
    public class VideoTrackAllViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public bool DefaultTrack { get; set; }
        public bool EnabledTrack { get; set; }
        public bool ForcedTrack { get; set; }
        public bool FlagHearingImpaired { get; set; }
        public bool FlagCommentary { get; set; }
        public Language Language { get; set; }
        public IExternalLibrariesService ExLib { get; }

        public VideoTrackAllViewModel(IExternalLibrariesService externalLibrariesService)
        {
            ExLib = externalLibrariesService;
        }
    }
}
