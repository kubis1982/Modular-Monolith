namespace ModularMonolith.Modules.Ordering.Domain.Exceptions
{

    internal class ConverterInvalidValueException : AppException
    {

        public ConverterInvalidValueException() : base($"Licznik i mianownik muszą być większe lub równe 1")
        {
        }
    }
}