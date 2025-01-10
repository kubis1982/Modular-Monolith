namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions
{
    using System;

    [Serializable]
    internal class CountryException() : AppException($"Dopuszczalny jest dwuliterowy skrót kraju (ISO 3166-1 alfa-2)")
    {
    }
}