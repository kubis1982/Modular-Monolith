namespace ModularMonolith.Shared.Serialization
{
    using System;

    public interface IJsonSerializer
    {
        string Serialize<T>(T value);
        T? Deserialize<T>(string value);
        object? Deserialize(string value, Type type);
        string Serialize(object? value);
    }
}
