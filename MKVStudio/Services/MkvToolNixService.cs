namespace MKVStudio.Services
{
    public class MkvToolNixService : IMkvToolNixService
    {
        private readonly IUtilitiesService _util;

        public MkvToolNixService(IUtilitiesService utilitiesService)
        {
            _util = utilitiesService;
        }
    }
}
