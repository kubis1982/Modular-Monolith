namespace ModularMonolith.Shared.Persistance.Converters
{
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using System.Collections.Generic;
    using System.Text.Json;

    internal class DictionaryStringConverter : ValueConverter<Dictionary<string, string>, string>
    {
        public DictionaryStringConverter() : base(
                n => JsonSerializer.Serialize(n, (JsonSerializerOptions?)null),
                n => JsonSerializer.Deserialize<Dictionary<string, string>>(n, (JsonSerializerOptions?)null) ?? new Dictionary<string, string>())
        {
        }
    }
}
