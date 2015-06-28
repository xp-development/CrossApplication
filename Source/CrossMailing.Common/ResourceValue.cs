using System;

namespace CrossMailing.Common
{
    public class ResourceValue
    {
        public Type Type { get; private set; }
        public string Key { get; private set; }

        public ResourceValue(Type type, string key)
        {
            Type = type;
            Key = key;
        }
    }
}