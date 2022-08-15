using Newtonsoft.Json;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Utility.Tools;

namespace SeaInk.Infrastructure.DataAccess.Serialization;

public class LayoutComponentSerializationConfiguration
{
    public LayoutComponentSerializationConfiguration(TypeLocator locator)
    {
        SerializationSettings = new JsonSerializerSettings
        {
            ContractResolver = new TypeKeyLoggingContractResolver<LayoutComponent>(locator),
        };
        DeserializationSettings = new JsonSerializerSettings
        {
            SerializationBinder = new TypeLocatorSerializationBinder(locator),
            TypeNameHandling = TypeNameHandling.All,
        };
    }

    public JsonSerializerSettings SerializationSettings { get; }
    public JsonSerializerSettings DeserializationSettings { get; }
}