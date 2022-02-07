using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SeaInk.Utility.Tools;

namespace SeaInk.Infrastructure.DataAccess.Serialization;

public class TypeKeyLoggingContractResolver<T> : DefaultContractResolver
{
    private const string Key = "$type";
    private readonly TypeLocator _typeLocator;

    public TypeKeyLoggingContractResolver(TypeLocator typeLocator)
    {
        _typeLocator = typeLocator;
    }

    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

        if (!type.IsAssignableTo(typeof(T)))
            return properties;

        string key = _typeLocator.GetKey(type);
        var property = new JsonProperty
        {
            DeclaringType = type,
            PropertyName = Key,
            UnderlyingName = Key,
            PropertyType = typeof(string),
            ValueProvider = new ConstantValueProvider(key),
            Writable = false,
            Readable = true,
            ItemIsReference = false,
            TypeNameHandling = TypeNameHandling.None,
        };

        properties.Insert(0, property);
        return properties;
    }

    private class ConstantValueProvider : IValueProvider
    {
        private readonly object _value;

        public ConstantValueProvider(object value)
        {
            _value = value;
        }

        public void SetValue(object target, object? value)
            => throw new NotSupportedException();

        public object GetValue(object target)
            => _value;
    }
}