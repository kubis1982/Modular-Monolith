﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModularMonolith.Modules.Contractors.Persistance.ReadModel.Entities;

[Table("Contractors", Schema = EntityType.ModuleCode)]
public partial class ContractorEntity {
    [StringLength(5)]
    public string TypeId { get; set; } = null!;

    [Key]
    public int Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    [StringLength(40)]
    public string Code { get; set; } = null!;

    [StringLength(80)]
    public string Name { get; set; } = null!;

    [StringLength(256)]
    public string? Description { get; set; }

    public bool IsBlocked { get; set; }

    [StringLength(ContractorRestriction.CountryLength)]
    public string? Country { get; set; }
}