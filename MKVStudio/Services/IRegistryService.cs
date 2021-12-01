using static MKVStudio.Services.RegistryService;

namespace MKVStudio.Services
{
    public interface IRegistryService
    {
        string GetExecutable(Executables executable);
    }
}