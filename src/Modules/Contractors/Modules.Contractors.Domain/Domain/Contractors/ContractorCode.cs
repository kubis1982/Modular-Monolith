namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    using ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions;
    using System.Text.RegularExpressions;

    public sealed partial record ContractorCode
    {
        public const string Pattern = "[a-zA-Z0-9_-]+";

        public string Value { get; private set; } = string.Empty;

        private ContractorCode()
        {
        }

        private ContractorCode(string? code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new IncorrectContractorCodeException();
            }
            if (!RegExCodePattern().IsMatch(code))
            {
                throw new CodeIncompatibleRegExRequirementsException(code);
            }
            Value = code ?? string.Empty;
        }

        public static ContractorCode Of(string? code) => new(code);

        public static implicit operator string(ContractorCode argument) => argument.Value;

        public static implicit operator ContractorCode(string code) => Of(code);

        [GeneratedRegex("^" + Pattern + "$")]
        private static partial Regex RegExCodePattern();
    }
}
