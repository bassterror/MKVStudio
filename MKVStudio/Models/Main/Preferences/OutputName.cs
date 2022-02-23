using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKVStudio.Models;

public class OutputName
{

    public bool AutomaticallySetTheDestinationFileName { get; set; } //TODO
    public bool OnlyUseTheFirstSourceFileWithAVideoTrack { get; set; } //TODO
    public bool UseTheTitleAsTheBaseFileNameIfATitleIsSet { get; set; } //TODO
    public bool SameDirectoryAsTheFirstSourceFile { get; set; } //TODO
    public bool PreviouslyUsedDestinationDirectory { get; set; } //TODO
    public bool DirectoryRelativeToFirstSourceFileDirectory { get; set; } //TODO
    public bool UseFixedDirectory { get; set; } //TODO
    public bool EnsureTheFileNameIsUnique { get; set; } //TODO
    public bool AutomaticallyClearTheDestinationFileNameWhenTheLastFileIsRemoved { get; set; } //TODO
}
