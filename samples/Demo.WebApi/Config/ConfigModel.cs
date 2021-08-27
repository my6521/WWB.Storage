using WWB.Storage;
using WWB.Storage.Aliyun;
using WWB.Storage.Config;
using WWB.Storage.Local;
using WWB.Storage.Tencent;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.WebApi.Config
{
    public class ConfigModel
    {
        [Required]
        public StorageProviderTypes ProviderType { get; set; }
        [Required]
        public LocalStorageConfig LocalCfg { get; set; }
        public AliyunOssConfig AliyunCfg { get; set; }
        public TencentCosConfig TencentCfg { get; set; }

        public StorageConfigBase GetCfg()
        {
            return ProviderType switch
            {
                StorageProviderTypes.Local => LocalCfg,
                StorageProviderTypes.Aliyun => AliyunCfg,
                StorageProviderTypes.Tencent => TencentCfg,
                _ => throw new ArgumentNullException()
            };
        }
    }
}
