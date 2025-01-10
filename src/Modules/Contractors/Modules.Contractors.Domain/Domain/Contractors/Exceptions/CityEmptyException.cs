namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions
{

    internal class CityEmptyException(string value) : FieldEmptyException("Miasto", value)
    {
    }
}
