namespace WWB.Storage.Model
{
    public class BlobFileInfo
    {
        /// <summary>
        /// 对象名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 存储桶名称
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        /// 访问主机
        /// </summary>
        public string BaseUrl { get; set; }
    }
}
