using System;

namespace CrossMailing.Common
{
    public class ResourceValue
    {
        public Type ResourceManagerType { get; private set; }
        public string Key { get; private set; }

        public ResourceValue(Type resourceManagerType, string key)
        {
            ResourceManagerType = resourceManagerType;
            Key = key;
        }
    }
}