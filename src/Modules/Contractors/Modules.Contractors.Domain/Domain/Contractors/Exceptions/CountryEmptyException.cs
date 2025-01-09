namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions
{

    internal class CountryEmptyException(string value) : FieldEmptyException("Kraj", value)
    {
    }
}
