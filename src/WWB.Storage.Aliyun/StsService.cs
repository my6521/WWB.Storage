using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Auth.Sts;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace WWB.Storage.Aliyun
{
    public class StsService : IStsService
    {
        private readonly StsClientConfig _config;
        private readonly IAcsClient _client;

        public StsService(StsClientConfig config)
        {
            _config = config;
            IClientProfile clientProfile = DefaultProfile.GetProfile(config.RegionId, config.AccessKeyId, config.Secret);
            _client = new DefaultAcsClient(clientProfile);
        }

        public AssumeRoleResponse GetSecurityToken()
        {
            //构建AssumeRole请求
            //指定角色ARN
            //设置Token有效期，可选参数，默认3600秒
            //设置Token的附加权限策略；在获取Token时，通过额外设置一个权限策略进一步减小Token的权限
            //request.Policy="<policy-content>"
            var request = new AssumeRoleRequest
            {
                AcceptFormat = FormatType.JSON,
                RoleArn = _config.RoleArn,
                RoleSessionName = _config.RoleSessionName,// "upload",
                DurationSeconds = 3600
            };
            var response = _client.GetAcsResponse(request);
            return response;
        }
    }
}