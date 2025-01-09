namespace ModularMonolith.Modules.Contractors.Persistance
{
    public static class ContractorRestriction
    {
        public const int CodeLength = 40;
        public const int NameLength = 80;
        public const int DescriptionLength = 256;
        public const string CodePattern = Domain.Contractors.ContractorCode.Pattern;
        public const int CountryLength = 2;

        public const int AddressLineLength = 50;
        public const int AddressCountryLength = CountryLength;
        public const int AddressCityLength = 30;
        public const int AddressPostalCodeLength = 20;
    }
}
