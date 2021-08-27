using Aliyun.OSS;
using WWB.Storage.Error;
using WWB.Storage.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WWB.Storage.Aliyun
{
    public class AliyunStorageProvider : IStorageProvider
    {
        private readonly AliyunOssConfig _cfg;
        private readonly OssClient _ossClient;

        public AliyunStorageProvider(IServiceProvider serviceProvider, AliyunOssConfig cfg)
        {
            _cfg = cfg;
            _ossClient = new OssClient(cfg.Endpoint, cfg.AccessKeyId, cfg.AccessKeySecret);
        }

        public async Task<BlobFileInfo> PutBlob(string blobName, Stream source)
        {
            try
            {
                await Task.Run(() =>
                {
                    _ossClient.PutObject(_cfg.BucketName, blobName, source).HandlerError("上传对象出错");
                });
            }
            catch (Exception ex)
            {
                throw new StorageException(StorageErrorCode.PostError.ToStorageError(), ex);
            }

            return new BlobFileInfo()
            {
                Name = blobName,
                BucketName = _cfg.BucketName,
                BaseUrl = GetUrlByKey(blobName)
            };
        }

        public async Task DeleteBlob(string blobName)
        {
            try
            {
                await Task.Run(() =>
                {
                    _ossClient.DeleteObject(_cfg.BucketName, blobName);
                });
            }
            catch (Exception ex)
            {
                throw new StorageException(StorageErrorCode.PostError.ToStorageError(), ex);
            }
        }

        public async Task DeleteBlob(string bucketName, string blobName)
        {
            try
            {
                await Task.Run(() =>
                {
                    _ossClient.DeleteObject(bucketName, blobName);
                });
            }
            catch (Exception ex)
            {
                throw new StorageException(StorageErrorCode.PostError.ToStorageError(), ex);
            }
        }

        private string GetUrlByKey(string key) => $"https://{_cfg.BucketName}.{_cfg.Endpoint}/{key}";
    }
}
