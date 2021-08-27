﻿using COSXML.Model;
using WWB.Storage.Error;
using System;
using System.Threading.Tasks;

namespace WWB.Storage.Tencent
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Extentions
    {
        /// <summary>
        /// 根据错误类型返回错误异常
        /// </summary>
        /// <returns></returns>
        public static Task HandlerError(this CosResult response, string friendlyMessage = null)
        {
            var code = (int)response.httpCode;
            if (code < 300 || code >= 600) return Task.FromResult(0);

            var message = response.httpMessage;
            throw new StorageException(
                new StorageError { Code = code, Message = friendlyMessage ?? message, ProviderMessage = message },
                new Exception($"腾讯云存储错误！"));
        }
    }
}
