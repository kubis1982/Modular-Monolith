namespace ModularMonolith.Modules.Articles.Domain.Articles
{
    using ModularMonolith.Modules.Articles.Domain.Articles.Exceptions;
    using System;
    using System.Text.RegularExpressions;

    public partial record ArticleCode
    {
        public const string Pattern = "[a-zA-Z0-9_-]+";

        public string Value { get; private set; } = string.Empty;

        private ArticleCode()
        {
        }

        private ArticleCode(string? code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException(nameof(ArticleCode));
            }
            if (!RegExCodePattern().IsMatch(code))
            {
                throw new CodeIncompatibleRegExRequirementsException(code);
            }
            Value = code ?? string.Empty;
        }

        public static ArticleCode Of(string? code) => new(code);

        public static implicit operator string(ArticleCode argument) => argument.Value;

        public static implicit operator ArticleCode(string code) => Of(code);

        [GeneratedRegex("^" + Pattern + "$")]
        private static partial Regex RegExCodePattern();
    }
}
