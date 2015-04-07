using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace CrossMailing.Common
{
    public static class LocalizationManager
    {
        public static string Get(Type resourceType, string name)
        {
            if(!ResourceManagersCache.ContainsKey(resourceType.FullName))
                ResourceManagersCache.Add(resourceType.FullName, new ResourceManager(resourceType.FullName, resourceType.GetTypeInfo().Assembly));

            return ResourceManagersCache[resourceType.FullName].GetString(name);
        }

        private static readonly Dictionary<string, ResourceManager> ResourceManagersCache = new Dictionary<string, ResourceManager>();
    }
}
