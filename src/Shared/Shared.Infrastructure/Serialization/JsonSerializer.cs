namespace ModularMonolith.Shared.Serialization
{
    using Microsoft.Extensions.Options;
    using System;
    using System.Text.Json;

    internal class JsonSerializer(IOptions<JsonSerializerOptions> options) : IJsonSerializer
    {
        public string Serialize<T>(T value) => System.Text.Json.JsonSerializer.Serialize<T>(value, options.Value);

        public string Serialize(object? value) => System.Text.Json.JsonSerializer.Serialize(value, options.Value);

        public T? Deserialize<T>(string value) => System.Text.Json.JsonSerializer.Deserialize<T>(value, options.Value);

        public object? Deserialize(string value, Type type) => System.Text.Json.JsonSerializer.Deserialize(value, type, options.Value);
    }
}
