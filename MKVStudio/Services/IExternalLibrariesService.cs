using MKVStudio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKVStudio.Services;

public interface IExternalLibrariesService
{
    Dictionary<ExecutableNames, Executable> Executables { get; set; }
    Task<ProcessResult> Run(ProcessResultNames processName, SourceFileInfo sourceFile = null, string attachmentId = null, string attachmentTempPath = null);
}
