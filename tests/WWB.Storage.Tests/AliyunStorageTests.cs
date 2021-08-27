using WWB.Storage.Aliyun;
using WWB.Storage.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WWB.Storage.Tests
{
    [Trait("Group", "阿里云oss测试")]
    public class AliyunStorageTests : TestBase
    {
        public AliyunStorageTests()
        {
            //填写自己的阿里云OSS
            var config = new AliyunOssConfig()
            {
                Endpoint = "",
                AccessKeyId = "",
                AccessKeySecret = "",
                BucketName = ""
            };

            StorageProvider = StorageProviderFactory.Create(config);
        }

        [Fact(DisplayName = "阿里云oss上传测试")]
        public async Task SaveBlob_Test()
        {
            await Assert.ThrowsAnyAsync<StorageException>(async () =>
            {
                await StorageProvider.PutBlob(GetTestFileName(), base.TestStream);
            });
        }

        [Fact(DisplayName = "阿里云oss删除测试")]
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
