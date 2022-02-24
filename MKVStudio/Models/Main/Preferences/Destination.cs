using MKVStudio.Services;
using Newtonsoft.Json;

namespace MKVStudio.Models;

[JsonObject(MemberSerialization.OptIn)]
public class Destination : BaseModel
{
    private bool _sameDirectoryAsTheFirstSourceFile;
    private bool _previouslyUsedDestinationDirectory;
    private bool _directoryRelativeToFirstSourceFileDirectory;
    private bool _useFixedDirectory;
    private bool _automaticallySetTheDestinationFileName;
    private bool _onlyUseTheFirstSourceFileWithAVideoTrack;
    private bool _useTheTitleAsTheBaseFileNameIfATitleIsSet;
    private bool _ensureTheFileNameIsUnique;
    private bool _automaticallyClearTheDestinationFileNameWhenTheLastFileIsRemoved;

    public IUtilitiesService Util { get; set; }
    [JsonProperty]
    public bool AutomaticallySetTheDestinationFileName
    {
        get => _automaticallySetTheDestinationFileName;
        set
        {
            _automaticallySetTheDestinationFileName = value;
            SaveChanges();
        }
    }

    [JsonProperty]
    public bool OnlyUseTheFirstSourceFileWithAVideoTrack
    {
        get => _onlyUseTheFirstSourceFileWithAVideoTrack;
        set
        {
            _onlyUseTheFirstSourceFileWithAVideoTrack = value;
            SaveChanges();
        }
    } //TODO

    [JsonProperty]
    public bool UseTheTitleAsTheBaseFileNameIfATitleIsSet
    {
        get => _useTheTitleAsTheBaseFileNameIfATitleIsSet;
        set
        {
            _useTheTitleAsTheBaseFileNameIfATitleIsSet = value;
            SaveChanges();
        }
    }

    [JsonProperty]
    public bool SameDirectoryAsTheFirstSourceFile
    {
        get => _sameDirectoryAsTheFirstSourceFile;
        set
        {
            _sameDirectoryAsTheFirstSourceFile = value;
            SaveChanges();
        }
    }

    [JsonProperty]
    public bool PreviouslyUsedDestinationDirectory
    {
        get => _previouslyUsedDestinationDirectory;
        set
        {
            _previouslyUsedDestinationDirectory = value;
            SaveChanges();
        }
    } //TODO should hold the last used

    [JsonProperty]
    public bool DirectoryRelativeToFirstSourceFileDirectory
    {
        get => _directoryRelativeToFirstSourceFileDirectory;
        set
        {
            _directoryRelativeToFirstSourceFileDirectory = value;
            SaveChanges();
        }
    } //TODO

    [JsonProperty]
    public bool UseFixedDirectory
    {
        get => _useFixedDirectory;
        set
        {
            _useFixedDirectory = value;
            SaveChanges();
        }
    } //TODO

    [JsonProperty]
    public bool EnsureTheFileNameIsUnique
    {
        get => _ensureTheFileNameIsUnique;
        set
        {
            _ensureTheFileNameIsUnique = value;
            SaveChanges();
        }
    }

    [JsonProperty]
    public bool AutomaticallyClearTheDestinationFileNameWhenTheLastFileIsRemoved
    {
        get => _automaticallyClearTheDestinationFileNameWhenTheLastFileIsRemoved;
        set
        {
            _automaticallyClearTheDestinationFileNameWhenTheLastFileIsRemoved = value;
            SaveChanges();
        }
    } //TODO

    private void SaveChanges()
    {
        if (Util != null)
        {
            Util.SavePreferences();
        }
    }
}
