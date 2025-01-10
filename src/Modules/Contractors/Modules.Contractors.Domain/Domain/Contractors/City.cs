namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    using ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions;

    public sealed record City
    {
        public string Value { get; private set; } = string.Empty;

        private City()
        {
        }

        public City(string? city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new CityEmptyException(city ?? string.Empty);
            }
            Value = city ?? string.Empty;
        }

        public static City Of(string? city) => new(city);

        public static implicit operator string(City argument) => argument.Value;

        public static implicit operator City(string country) => Of(country);
    }
}
