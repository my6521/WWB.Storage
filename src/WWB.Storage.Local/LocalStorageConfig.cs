using WWB.Storage.Attributes;
using WWB.Storage.Config;
using System.ComponentModel.DataAnnotations;

namespace WWB.Storage.Local
{
    [ConfigFlag(StorageProviderTypes.Local)]
    public class LocalStorageConfig : StorageConfigBase
    {

        /// <summary>
        /// 存储桶名称
        /// </summary>
        [Required]
        public string BucketName { get; set; }

        /// <summary>
        /// 根Url
        /// </summary>
        [Required]
        public string BaseUrl { get; set; }
    }
}
