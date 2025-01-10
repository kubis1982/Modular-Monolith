namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses.Exceptions
{
    internal class CodeIncompatibleRegExRequirementsException : AppException
    {
        public CodeIncompatibleRegExRequirementsException(string code) : base($"W kodzie {code} znajdują się niedozwolone znaki. Nie używaj spacji, znaków diakretycznych oraz znaków specjalnych poza podkreślnikiem i myślnikiem.")
        {
        }
    }
}
