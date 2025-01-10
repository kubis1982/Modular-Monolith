namespace ModularMonolith.Modules.Warehouses.Api
{
    using System.ComponentModel.DataAnnotations;

    public class EntityIdentityResultDto
    {
        [MaxLength(5)]
        public required string TypeId { get; set; }
        public required int Id { get; set; }
    }
}