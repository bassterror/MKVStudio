namespace MKVStudio.Services
{
    public class FfmpegService : IFfmpegService
    {
        public IRegistryService _registry;

        public FfmpegService(IRegistryService registryService)
        {
            _registry = registryService;
        }
    }
}
