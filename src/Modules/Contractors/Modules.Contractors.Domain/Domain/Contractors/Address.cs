namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    public record Address
    {
        public string? Line1 { get; private set; } = string.Empty;
        public string? Line2 { get; private set; } = string.Empty;
        public string? PostalCode { get; private set; } = string.Empty;
        public City City { get; private set; }
        public Country Country { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Address() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private Address(string? line1, string? line2, string? postalCode, City city, Country country)
        {
            Line1 = line1;
            Line2 = line2;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public static Address Create(string? line1, string? line2, string? postalCode, City city, Country country)
        {
            return new(line1, line2, postalCode, city, country);
        }
    }
}
