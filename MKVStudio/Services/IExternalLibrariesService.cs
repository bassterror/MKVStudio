using MKVStudio.Models;
using System.Threading.Tasks;

namespace MKVStudio.Services
{
    public interface IExternalLibrariesService
    {
        Task<ProcessResult> RunProcess(RegistryService.Executables executable, string arguments, ProcessResultNames processName);
    }
}