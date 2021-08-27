using WWB.Storage.Model;
using System.IO;
using System.Threading.Tasks;

namespace WWB.Storage
{
    public interface IStorageProvider
    {
        Task<BlobFileInfo> PutBlob(string blobName, Stream source);
        Task DeleteBlob(string blobName);
        Task DeleteBlob(string bucketName, string blobName);
    }
}
