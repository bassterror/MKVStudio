using System;

namespace MKVStudio.Models
{
    public enum ProcessResultNames
    {
        LoudnormFirst,
        LoudnormSecondStereo,
        LoudnormSecond6Channels,
        MKVInfo,
        MKVExtract,
        MKVMerge
    }

    public class ProcessResult
    {
        public ProcessResultNames Name { get; set; }
        public string StdOutput { get; set; }
        public string StdErrOutput { get; set; }
        public int ExitCode { get; set; }
        public Exception Exception { get; set; }
    }
}
