using System;
using System.Collections.Generic;
using System.Text;

namespace WWB.Storage.Aliyun
{
    public class StsClientConfig
    {
        public string RegionId { get; set; }

        public string AccessKeyId { get; set; }

        public string Secret { get; set; }

        public string RoleArn { get; set; }

        public string RoleSessionName { get; set; }
    }
}