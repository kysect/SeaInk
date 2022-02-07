namespace SeaInk.Infrastructure.DataAccess.Models;

public class TypedSerializedObject
{
    public TypedSerializedObject(string type, string data)
    {
        Type = type;
        Data = data;
    }

    public string Type { get; }
    public string Data { get; }
}