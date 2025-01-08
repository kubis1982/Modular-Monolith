namespace ModularMonolith.Modules.Articles.Persistance.ReadModel.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MeasurementUnits", Schema = EntityType.ModuleCode)]
    public partial class MeasurementUnitEntity
    {
        [StringLength(5)]
        [Unicode(false)]
        public string TypeId { get; set; } = null!;

        [Key]
        public int Id { get; set; }

        [Precision(2)]
        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        [Precision(2)]
        public DateTime? ModifiedOn { get; set; }

        public int? ModifiedBy { get; set; }

        [StringLength(MeasurementUnitRestriction.NameLength)]
        public string Name { get; set; } = null!;

        [StringLength(MeasurementUnitRestriction.CodeLength)]
        public string Code { get; set; } = null!;
    }
}
