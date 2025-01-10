namespace ModularMonolith.Shared.Exceptions
{
    public class CodeEmptyException(string objectName) : FieldEmptyException("Kod", objectName)
    {
    }
}
