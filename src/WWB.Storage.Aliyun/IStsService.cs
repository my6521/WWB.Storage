using Aliyun.Acs.Core.Auth.Sts;
using System;
using System.Collections.Generic;
using System.Text;

namespace WWB.Storage.Aliyun
{
    public interface IStsService
    {
        AssumeRoleResponse GetSecurityToken();
    }
}