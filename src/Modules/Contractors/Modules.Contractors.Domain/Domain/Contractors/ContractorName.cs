namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    using ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions;

    public sealed record ContractorName
    {
        public string Value { get; private set; } = string.Empty;

        private ContractorName()
        {
        }

        private ContractorName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new IncorrectContractorNameException();
            }
            Value = name ?? string.Empty;
        }

        public static ContractorName Of(string? name) => new(name);

        public static implicit operator string(ContractorName argument) => argument.Value;

        public static implicit operator ContractorName(string name) => Of(name);
    }
}
