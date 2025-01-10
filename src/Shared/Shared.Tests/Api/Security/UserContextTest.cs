namespace ModularMonolith.Shared.Api.Security
{
    using ModularMonolith.Shared.Security;

    public class UserContextTest : IUserContext
    {
        public int Id { get; set; } = 1;

        public string Name { get; set; } = "admin";

        public int SessionId { get; set; } = 1;

        public string Email { get; set; } = "admin@modular-monolith.pl";
    }
}
