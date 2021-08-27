using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using COSXML.Utils;
using WWB.Storage.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WWB.Storage.Tencent
{
    public class TencentStorageProvider : IStorageProvider
    {
        private readonly TencentCosConfig _cfg;
        private readonly CosXmlServer _cosXmlServer;

        public TencentStorageProvider(IServiceProvider serviceProvider, TencentCosConfig cfg)
        {
            _cfg = cfg;

            var config = new CosXmlConfig.Builder()
              .SetConnectionTimeoutMs(60000)  //设置连接超时时间，单位毫秒，默认45000ms
              .SetReadWriteTimeoutMs(40000)  //设置读写超时时间，单位毫秒，默认45000ms
              .IsHttps(true)  //设置默认 HTTPS 请求
              .SetAppid(cfg.AppId)  //设置腾讯云账户的账户标识 APPID
              .SetRegion(cfg.Region)  //设置一个默认的存储桶地域
              .SetDebugLog(true)  //显示日志
              .Build();  //创建 CosXmlConfig 对象

            //初始化 QCloudCredentialProvider，COS SDK 中提供了3种方式：永久密钥、临时密钥、自定义
            QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(cfg.SecretId, cfg.SecretKey, 600);

            //初始化 CosXmlServer
            _cosXmlServer = new CosXmlServer(config, cosCredentialProvider);
        }

        public async Task<BlobFileInfo> PutBlob(string blobName, Stream source)
        {
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                source.CopyTo(ms);
                bytes = ms.ToArray();
            }

            var request = new PutObjectRequest(_cfg.BucketName, blobName, bytes)
            {
            };

            var response = _cosXmlServer.PutObject(request);

            await response.HandlerError("上传对象出错!");

            return new BlobFileInfo() {
                Name = blobName,
                BucketName =_cfg.BucketName,
                BaseUrl = GetUrlByKey(blobName)
            };
        }

        public async Task DeleteBlob(string blobName)
        {
            var request = new DeleteObjectRequest(_cfg.BucketName, blobName);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.Seconds), 600);
            var response = _cosXmlServer.DeleteObject(request);
            await response.HandlerError("删除对象出错!");
        }

        public async Task DeleteBlob(string bucketName, string blobName)
        {
            var request = new DeleteObjectRequest(bucketName, blobName);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.Seconds), 600);
            var response = _cosXmlServer.DeleteObject(request);
            await response.HandlerError("删除对象出错!");
        }

        private string GetUrlByKey(string key) => $"https://{_cfg.BucketName}.cos.{_cfg.Region}.myqcloud.com/{key}";

    }
}
