using System;
using Newtonsoft.Json.Serialization;
using SeaInk.Utility.Tools;

namespace SeaInk.Infrastructure.DataAccess.Serialization;

public class TypeLocatorSerializationBinder : DefaultSerializationBinder
{
    private readonly TypeLocator _typeLocator;

    public TypeLocatorSerializationBinder(TypeLocator typeLocator)
    {
        _typeLocator = typeLocator;
    }

    public override Type BindToType(string? assemblyName, string typeName)
    {
        if (_typeLocator.TryGetType(typeName, out Type? type))
            return type!;

        return base.BindToType(assemblyName, typeName);
    }
}