using MKVStudio.Models;
using MKVStudio.ViewModels;
using System.Threading.Tasks;
using static MKVStudio.Services.ExternalLibrariesService;

namespace MKVStudio.Services;

public interface IExternalLibrariesService
{
    string GetExecutable(Executables executable);
    Task<ProcessResult> Run(ProcessResultNames processName, SourceFileInfo sourceFile = null, string attachmentId = null, string attachmentTempPath = null);
}
