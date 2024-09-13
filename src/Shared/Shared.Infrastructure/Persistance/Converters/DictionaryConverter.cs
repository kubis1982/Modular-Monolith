namespace Kubis1982.Shared.Persistance.Converters
{
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using System.Collections.Generic;
    using System.Text.Json;

    public class DictionaryConverter : ValueConverter<Dictionary<string, object?>, string>
    {
        public DictionaryConverter() : base(
                n => JsonSerializer.Serialize(n, (JsonSerializerOptions?)null),
                n => JsonSerializer.Deserialize<Dictionary<string, object?>>(n, new JsonSerializerOptions
                {
                    Converters = { new ObjectConverter() }
                }) ?? new Dictionary<string, object?>())
        {
        }
    }
}
