﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace ModularMonolith.Modules.Ordering.Persistance.ReadModel;

[Table("Orders", Schema = EntityType.ModuleCode)]
public partial class OrderEntity {
    [StringLength(5)]
    public string TypeId { get; set; } = null!;

    [Key]
    public int Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    [StringLength(5)]
    public string ContractorTypeId { get; set; } = null!;

    public int ContractorId { get; set; }

    [StringLength(5)]
    public string WarehouseTypeId { get; set; } = null!;

    public int WarehouseId { get; set; }

    public DateTime? ExecutionDate { get; set; }

    public byte Status { get; set; }
    public string? OrderNo { get; set; }
    public byte OrderType { get; set; }
    public string? Description { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    [StringLength(OrderRestriction.AddressPostalCodeLength)]
    public string? AddressPostalCode { get; set; }
    [StringLength(OrderRestriction.AddressCityLength)]
    public string? AddressCity { get; set; }
    [StringLength(OrderRestriction.AddressCountryLength)]
    public string? AddressCountry { get; set; }
}