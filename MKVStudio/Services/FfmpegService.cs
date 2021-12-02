namespace MKVStudio.Services
{
    public class FfmpegService : IFfmpegService
    {
        public IUtilitiesService _util;

        public FfmpegService(IUtilitiesService utilitiesService)
        {
            _util = utilitiesService;
        }
    }
}
