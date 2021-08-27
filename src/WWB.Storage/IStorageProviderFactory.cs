using WWB.Storage.Config;

namespace WWB.Storage
{
    public interface IStorageProviderFactory
    {
        IStorageProvider Create(StorageConfigBase config);
    }
}
