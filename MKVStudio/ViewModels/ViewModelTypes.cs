namespace MKVStudio.ViewModels
{
    public enum ViewModelTypes
    {
        //Main
        Files,
        Queue,

        //Video file
        Tracks,
        Attachments,
        FileOverview,
        AudioEdit,
        VideoEdit,
        Tags,

        //Video file tracks
        VideoTrack,
        AudioTrack,
        SubtitlesTrack,

        //Video file tags
        GobalTag,
        TrackTag,

        //Apply to all
        ATATracks,
        ATAAttachments,
        ATAFileOverview,
        ATAAudioEdit,
        ATAVideoEdit,
        ATATags,

        //Apply to all tracks
        ATAVideoTrack,
        ATAAudioTrack,
        ATASubtitleTrack,

        //Apply to all attachments
        ATAAttachment,

        //Apply to all tags
        ATAGlobalTag,
        ATATrackTag
    }
}
