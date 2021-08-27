using System;

namespace WWB.Storage.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigFlagAttribute : Attribute
    {
        public StorageProviderTypes ProviderType { get; private set; }

        public ConfigFlagAttribute(StorageProviderTypes providerType)
        {
            ProviderType = providerType;
        }
    }
}
