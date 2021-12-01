using System.Threading.Tasks;

namespace MKVStudio.Services
{
    public class MkvToolNixService : IMkvToolNixService
    {
        private readonly IRegistryService _registry;

        public MkvToolNixService(IRegistryService registryService)
        {
            _registry = registryService;
        }

        public async Task RunMKVInfo()
        {

        }
    }
}
