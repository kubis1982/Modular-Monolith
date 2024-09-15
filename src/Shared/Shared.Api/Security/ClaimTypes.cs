namespace ModularMonolith.Shared.Security
{
    public class ClaimTypes
    {
        public const string UserId = System.Security.Claims.ClaimTypes.NameIdentifier;
        public const string Name = System.Security.Claims.ClaimTypes.Name;
        public const string Email = System.Security.Claims.ClaimTypes.Email;
        public const string SessionId = $"{SystemInformation.SystemName}.SessionId";
    }
}
