namespace MKVStudio.ViewModels
{
    public enum ViewModelTypes
    {
        //Main
        Multiplexer,
        JobQueue,

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
        TrackTag
    }
}
