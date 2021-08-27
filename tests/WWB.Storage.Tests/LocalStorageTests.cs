using WWB.Storage.Error;
using WWB.Storage.Local;
using System.Threading.Tasks;
using Xunit;

namespace WWB.Storage.Tests
{
    [Trait("Group", "本地存储测试")]
    public class LocalStorageTests : TestBase
    {
        public LocalStorageTests()
        {
            var config = new LocalStorageConfig()
            {
                BucketName = base.Bucket,
                BaseUrl = "https://localhost/"
            };

            StorageProvider = StorageProviderFactory.Create(config);
        }

        [Fact(DisplayName = "本地文件上传测试")]
        public async Task SaveBlob_Test()
        {
            await Assert.ThrowsAnyAsync<StorageException>(async () =>
            {
                await StorageProvider.PutBlob(GetTestFileName(), base.TestStream);
            });
        }

        [Fact(DisplayName = "本地文件删除测试")]
        public async Task DeleteBlob_Test()
        {
            await Assert.ThrowsAnyAsync<StorageException>(async () =>
            {
                var fileName = GetTestFileName();
                await StorageProvider.PutBlob(fileName, base.TestStream);
                await StorageProvider.DeleteBlob(fileName);
            });
        }

    }
}
