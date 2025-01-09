namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    using ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions;

    public sealed record Country
    {
        public string Value { get; private set; } = string.Empty;

        private Country()
        {
        }

        public Country(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                throw new CountryEmptyException(nameof(Country));
            }
            try
            {
                var regionInfo = new System.Globalization.RegionInfo(country);
                country = regionInfo.Name;
            }
            catch (System.ArgumentException)
            {
                throw new CountryException();
            }

            Value = country ?? string.Empty;
        }

        public static Country Of(string? country) => new(country ?? string.Empty);

        public static implicit operator string(Country argument) => argument.Value;

        public static implicit operator Country(string country) => Of(country);
    }
}
