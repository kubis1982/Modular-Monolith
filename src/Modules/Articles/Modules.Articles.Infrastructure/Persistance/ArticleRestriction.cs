namespace ModularMonolith.Modules.Articles.Persistance
{

    public static class ArticleRestriction
    {
        public const int CodeLength = 40;
        public const int NameLength = 80;
        public const int UnitLength = MeasurementUnitRestriction.CodeLength;
        public const int DescriptionLength = 2048;
        public const int LanguageLength = 10;
        public const int TranslationLength = 255;
        public const string CodePattern = ArticleCode.Pattern;
    }
}
