namespace ModularMonolith.Modules.Ordering.Domain
{
    public record ArticleId(EntityTypeId TypeId, int Id);

    public record Article(ArticleId Id, string Code, string Name, string Unit, bool IsBlocked)
    {
        public static implicit operator ArticleId(Article article) => article.Id;
    }

    public record ContractorId(EntityTypeId TypeId, int Id);

    public record Contractor(ContractorId Id, string Code, string Name, bool IsBlocked)
    {
        public static implicit operator ContractorId(Contractor contractor) => contractor.Id;
    }

    public record WarehouseId(EntityTypeId TypeId, int Id);

    public record Warehouse(WarehouseId Id, string Code, string Name, bool IsBlocked)
    {
        public static implicit operator WarehouseId(Warehouse warehouse) => warehouse.Id;
    }

    public record Address
    {
        public string? Line1 { get; private set; } = string.Empty;
        public string? Line2 { get; private set; } = string.Empty;
        public string? PostalCode { get; private set; } = string.Empty;
        public string? City { get; private set; } = string.Empty;
        public string? Country { get; private set; } = string.Empty;

        public static Address Create(string? line1, string? line2, string? postalCode, string? city, string? country)
        {
            return new()
            {
                Line1 = line1 ?? string.Empty,
                Line2 = line2 ?? string.Empty,
                PostalCode = postalCode ?? string.Empty,
                City = city ?? string.Empty,
                Country = country ?? string.Empty
            };
        }

        public static Address Empty => new();
    }
}
