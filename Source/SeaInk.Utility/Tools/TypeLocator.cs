using System;
using System.Collections.Concurrent;

namespace SeaInk.Utility.Tools
{
    public class TypeLocator
    {
        private readonly ConcurrentDictionary<string, Type> _dictionary = new ConcurrentDictionary<string, Type>();

        public string GetKey(Type type)
            => type.Name;

        public TypeLocator AddTypes(params Type[] types)
        {
            foreach (Type type in types)
            {
                _dictionary.TryAdd(GetKey(type), type);
            }

            return this;
        }

        public bool TryGetType(string key, out Type? type)
            => _dictionary.TryGetValue(key, out type);
    }
}