using WWB.Storage.Attributes;
using WWB.Storage.Config;
using WWB.Storage.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WWB.Storage
{
    public class StorageProviderFactory : IStorageProviderFactory
    {
        public const string ASSEMBLY = "WWB.Storage.{0}";
        private static Dictionary<StorageProviderTypes, Type> _providerTypeDic = new Dictionary<StorageProviderTypes, Type>();
        private readonly IServiceProvider _serviceProvider;

        public StorageProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IStorageProvider Create(StorageConfigBase config)
        {
            var providerType = config.GetType().GetCustomAttribute<ConfigFlagAttribute>().ProviderType;
            if (!_providerTypeDic.ContainsKey(providerType))
            {
                var type = GetProviderType(providerType);
                _providerTypeDic.Add(providerType, type);
            }

            return (IStorageProvider)Activator.CreateInstance(_providerTypeDic[providerType], new object[] { _serviceProvider, config });
        }

        private Type GetProviderType(StorageProviderTypes providerType)
        {
            var assembly = Assembly.Load(string.Format(ASSEMBLY, providerType.ToString()));
            if (assembly == null)
                throw new StorageException(StorageErrorCode.ProviderNotFound.ToStorageError());

            var type = assembly.GetTypes().Where(type => typeof(IStorageProvider).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract).FirstOrDefault();
            if (type == null)
                throw new StorageException(StorageErrorCode.ProviderNotFound.ToStorageError());

            return type;
        }
    }
}
