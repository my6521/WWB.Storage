using WWB.Storage.Error;
using WWB.Storage.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace WWB.Storage
{
    public static class Extensions
    {
        public static IServiceCollection AddStrorage(this IServiceCollection services)
        {
            services.AddSingleton<IStorageProviderFactory, StorageProviderFactory>();

            return services;
        }

        /// <summary>
        /// 根据错误类型返回错误异常
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static StorageError ToStorageError(this StorageErrorCode code) => new StorageError()
        {
            Code = (int)code,
            Message = code.GetDisplayContent()
        };
    }
}
