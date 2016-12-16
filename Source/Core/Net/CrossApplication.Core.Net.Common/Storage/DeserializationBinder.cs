using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace CrossApplication.Core.Net.Common.Storage
{
    public class DeserializationBinder : SerializationBinder
    {
        private readonly Type _type;

        public DeserializationBinder(Type type)
        {
            _type = type;
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            return _type;
        }
    }
}