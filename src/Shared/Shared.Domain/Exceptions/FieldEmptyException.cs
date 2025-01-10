namespace ModularMonolith.Shared.Exceptions
{
    public class FieldEmptyException(string fieldName, string objectName) : AppException($"Pole `{fieldName}` jest wymagane ({objectName})")
    {
    }
}
