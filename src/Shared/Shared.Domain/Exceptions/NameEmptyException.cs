namespace ModularMonolith.Shared.Exceptions
{
    public class NameEmptyException(string objectName) : FieldEmptyException("Nazwa", objectName)
    {
    }
}
