namespace MKVStudio.ViewModels
{
    public enum ViewModelTypes
    {
        //Main
        Multiplexer,
        JobQueue,

        //Multiplex
        Input,
        Output,
        Attachments,
        Chapters,
        AudioEdit,
        VideoEdit,

        //Video file tracks
        VideoTrack,
        AudioTrack,
        SubtitlesTrack,

        //Video file tags
        GobalTag,
        TrackTag
    }
}
