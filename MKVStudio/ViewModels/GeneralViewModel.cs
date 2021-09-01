namespace MKVStudio.ViewModels
{
    public class GeneralViewModel : BaseViewModel
    {
        public FfmpegViewModel FfmpegViewModel { get; set; }

        public GeneralViewModel(FfmpegViewModel ffmpegViewModel)
        {
            FfmpegViewModel = ffmpegViewModel;
        }

    }
}
