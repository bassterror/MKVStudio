using MKVStudio.Models;
using MKVStudio.Services;

namespace MKVStudio.ViewModels
{
    public class FfmpegViewModel
    {
        private readonly IFfmpegService _ffmpegService;
        private readonly string _arguments;
        private readonly string _processName;

        public ProcessResult ProcessResult { get; set; }

        public FfmpegViewModel(IFfmpegService ffmpegService, string arguments, string processName)
        {
            _ffmpegService = ffmpegService;
            _arguments = arguments;
            _processName = processName;
        }
        public static FfmpegViewModel LoadFfmpegViewModel(IFfmpegService ffmpegService, string arguments, string processName)
        {
            FfmpegViewModel ffmpegViewModel = new FfmpegViewModel(ffmpegService, arguments, processName);
            ffmpegViewModel.LoadFfmpeg();
            return ffmpegViewModel;
        }

        private void LoadFfmpeg()
        {
            _ffmpegService.RunFFMPEG(_arguments, _processName).ContinueWith(task =>
             {
                 if (task.Exception == null)
                 {
                     ProcessResult = task.Result;
                 }
             });
        }
    }
}
