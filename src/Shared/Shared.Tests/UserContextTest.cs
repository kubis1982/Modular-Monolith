namespace ModularMonolith.Shared
{
    using ModularMonolith.Shared.Security;

    public class UserContextTest : IUserContext
    {
        public int SessionId { get; set; } = 1;

        public int Id { get; set; } = 1;

        public string Email { get; set; } = $"administrator@{SystemInformation.DomainName}";

        public string Name { get; set; } = $"administrator@{SystemInformation.DomainName}";
    }
}
