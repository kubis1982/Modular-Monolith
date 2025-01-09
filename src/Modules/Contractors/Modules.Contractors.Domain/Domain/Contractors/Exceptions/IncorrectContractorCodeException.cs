namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions
{
    public sealed class IncorrectContractorCodeException : AppException
    {
        public IncorrectContractorCodeException() : base("Kod kontrahenta nie może być pusty")
        {
        }
    }
}
