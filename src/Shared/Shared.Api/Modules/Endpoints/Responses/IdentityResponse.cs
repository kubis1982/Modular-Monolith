namespace ModularMonolith.Shared.Modules.Endpoints.Responses
{
    using ModularMonolith.Shared.CQRS.Commands;

    public class IdentityResponse
    {
        public string TypeId { get; set; } = string.Empty;
        public int Id { get; set; }

        public static implicit operator IdentityResponse(EntityIdentityResult argument) => new() { TypeId = argument.TypeId.Value, Id = argument.Id };
    }
}
