namespace ModularMonolith.Modules.Articles.Domain.MeasurementUnits.Exceptions
{

    internal class MeasurementUnitCodeIncompatiblePatternException(string code) : AppException($"Kod {code} jest niezgodny ze wzorcem ({MeasurementUnitCode.Pattern}).")
    {
    }
}
