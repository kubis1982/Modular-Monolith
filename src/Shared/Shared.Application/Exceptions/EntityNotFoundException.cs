namespace Kubis1982.Shared.Exceptions
{
    public class EntityNotFoundException : AppException
    {
        public EntityNotFoundException() : base("Failed to find entity in the system!")
        {
        }

        public EntityNotFoundException(string objectName) : base($"Failed to find entity in the system (`{objectName}`)!")
        {
        }
    }
}
