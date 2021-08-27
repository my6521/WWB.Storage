using WWB.Storage.Error;
using WWB.Storage.Tencent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WWB.Storage.Tests
{
    [Trait("Group", "腾讯云COS测试")]
    public class TencentStorageTests : TestBase
    {
        public TencentStorageTests()
        {
            //填写自己的腾讯云cos配置
            var config = new TencentCosConfig()
            {
               AppId = "",
               SecretId ="",
               SecretKey = "",
               Region ="",
               BucketName =base.Bucket,
            };

            StorageProvider = StorageProviderFactory.Create(config);
        }


        [Fact(DisplayName = "腾讯云COS上传测试")]
        public async Task SaveBlob_Test()
        {
            await Assert.ThrowsAnyAsync<StorageException>(async () =>
            {
                await StorageProvider.PutBlob(GetTestFileName(), base.TestStream);
            });
        }

        [Fact(DisplayName = "腾讯云COS删除测试")]
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
