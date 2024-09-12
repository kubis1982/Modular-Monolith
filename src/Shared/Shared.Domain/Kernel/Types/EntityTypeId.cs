namespace Kubis1982.Shared.Kernel.Types
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public record EntityTypeId
    {
        [StringLength(5)]
        public string Value { get; }

        private EntityTypeId()
        {
            Value = string.Empty;
        }

        public EntityTypeId(string code)
        {
            if (code.Length != 5)
            {
                throw new ArgumentException($"Code '{code}' must be 5 characters long");
            }
            Value = code;
        }

        private EntityTypeId(string prefix, short numerator)
        {
            if (prefix.Length != 3)
            {
                throw new ArgumentException($"Prefix '{prefix}' must be 3 characters long");
            }
            if (numerator <= 0 || numerator > 99)
            {
                throw new ArgumentException($"Numerator '{numerator}' must be in range 1-99");
            }
            Value = $"{prefix}{numerator:D2}";
        }

        [JsonIgnore]
        public short Numerator
        {
            get
            {
                if (Value.Length == 5)
                {
                    _ = short.TryParse(Value[3..5], out short numerator);
                    return numerator;
                }
                return 0;
            }
        }

        [JsonIgnore]
        public string Prefix
        {
            get
            {
                if (Value.Length == 6)
                {
                    return Value[0..3];
                }
                return string.Empty;
            }
        }

        public static implicit operator string(EntityTypeId? entityTypeCode) => entityTypeCode?.Value ?? string.Empty;
        public static implicit operator EntityTypeId(string? code) => string.IsNullOrEmpty(code) ? Empty : new(code);

        public static EntityTypeId Create(string prefix, short numerator) => new(prefix, numerator);

        public static EntityTypeId Empty => new();

        public override string ToString() => Value;
    }
}
