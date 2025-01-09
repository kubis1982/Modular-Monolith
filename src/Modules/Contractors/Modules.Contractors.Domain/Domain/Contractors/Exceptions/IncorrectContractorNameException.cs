namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions
{
    public sealed class IncorrectContractorNameException : AppException
    {
        public IncorrectContractorNameException() : base("Nazwa kontrahenta nie może być pusta")
        {
        }
    }
}
