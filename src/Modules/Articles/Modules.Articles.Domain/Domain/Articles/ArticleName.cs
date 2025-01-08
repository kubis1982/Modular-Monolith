namespace ModularMonolith.Modules.Articles.Domain.Articles
{
    using System;

    public record ArticleName
    {
        public string Value { get; private set; } = string.Empty;

        private ArticleName()
        {
        }

        private ArticleName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is empty", nameof(ArticleName));
            }
            Value = name ?? string.Empty;
        }

        public static ArticleName Of(string? name) => new(name);

        public static implicit operator string(ArticleName argument) => argument.Value;

        public static implicit operator ArticleName(string name) => Of(name);
    }
}
