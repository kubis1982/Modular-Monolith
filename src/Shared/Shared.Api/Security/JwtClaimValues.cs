namespace ModularMonolith.Shared.Security
{
    public class JwtClaimValues
    {
        public required int UserId { get; set; }
        public required string UserEmail { get; set; }
        public required int SessionId { get; set; }
        public required string UserName { get; set; }
    }
}
