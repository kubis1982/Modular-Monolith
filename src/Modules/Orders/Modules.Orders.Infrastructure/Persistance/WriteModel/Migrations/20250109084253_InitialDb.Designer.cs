﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModularMonolith.Modules.Orders.Persistance.WriteModel;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ModularMonolith.Modules.Orders.Persistance.WriteModel.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    [Migration("20250109084253_InitialDb")]
    partial class InitialDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("OrM")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ModularMonolith.Modules.Ordering.Domain.DocumentNumber", b =>
                {
                    b.Property<string>("EntityTypeId")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("character(5)")
                        .IsFixedLength();

                    b.Property<short>("Year")
                        .HasColumnType("smallint");

                    b.Property<byte>("Month")
                        .HasColumnType("smallint");

                    b.Property<byte>("Day")
                        .HasColumnType("smallint");

                    b.Property<short>("Numerator")
                        .HasColumnType("smallint");

                    b.HasKey("EntityTypeId", "Year", "Month", "Day");

                    b.ToTable("DocumentNumbers", "OrM");
                });

            modelBuilder.Entity("ModularMonolith.Modules.Ordering.Domain.Orders.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(2);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnOrder(4);

                    b.Property<DateTime?>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(2)
                        .HasColumnType("timestamp(2) with time zone")
                        .HasColumnOrder(3)
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Description")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<DateTime>("ExecutionDate")
                        .HasPrecision(2)
                        .HasColumnType("timestamp(2) with time zone");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("integer")
                        .HasColumnOrder(6);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasPrecision(2)
                        .HasColumnType("timestamp(2) with time zone")
                        .HasColumnOrder(5);

                    b.Property<string>("OrderNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<byte>("OrderType")
                        .HasColumnType("smallint");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint");

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("character(5)")
                        .HasColumnOrder(1)
                        .IsFixedLength();

                    b.Property<Guid?>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid")
                        .HasColumnOrder(7);

                    b.HasKey("Id");

                    b.ToTable("Orders", "OrM");

                    b.HasDiscriminator<string>("TypeId");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ModularMonolith.Modules.Ordering.Domain.Orders.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(2);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnOrder(4);

                    b.Property<DateTime?>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(2)
                        .HasColumnType("timestamp(2) with time zone")
                        .HasColumnOrder(3)
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Description")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("integer")
                        .HasColumnOrder(6);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasPrecision(2)
                        .HasColumnType("timestamp(2) with time zone")
                        .HasColumnOrder(5);

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<short>("Position")
                        .HasColumnType("smallint");

                    b.Property<decimal?>("QuantityCompleted")
                        .HasPrecision(14, 4)
                        .HasColumnType("numeric(14,4)");

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("character(5)")
                        .HasColumnOrder(1)
                        .IsFixedLength();

                    b.Property<Guid?>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid")
                        .HasColumnOrder(7);

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("Id", "Position");

                    b.ToTable("OrderItems", "OrM");

                    b.HasDiscriminator<string>("TypeId");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ModularMonolith.Modules.Ordering.Domain.Orders.PurchaseOrder", b =>
                {
                    b.HasBaseType("ModularMonolith.Modules.Ordering.Domain.Orders.Order");

                    b.HasDiscriminator().HasValue("OrM01");
                });

            modelBuilder.Entity("ModularMonolith.Modules.Ordering.Domain.Orders.SalesOrder", b =>
                {
                    b.HasBaseType("ModularMonolith.Modules.Ordering.Domain.Orders.Order");

                    b.HasDiscriminator().HasValue("OrM03");
                });

            modelBuilder.Entity("ModularMonolith.Modules.Ordering.Domain.Orders.PurchaseOrderItem", b =>
                {
                    b.HasBaseType("ModularMonolith.Modules.Ordering.Domain.Orders.OrderItem");

                    b.HasDiscriminator().HasValue("OrM02");
                });

            modelBuilder.Entity("ModularMonolith.Modules.Ordering.Domain.Orders.SalesOrderItem", b =>
                {
                    b.HasBaseType("ModularMonolith.Modules.Ordering.Domain.Orders.OrderItem");

                    b.HasDiscriminator().HasValue("OrM04");
                });

            modelBuilder.Entity("ModularMonolith.Modules.Ordering.Domain.Orders.Order", b =>
                {
                    b.OwnsOne("ModularMonolith.Modules.Ordering.Domain.Address", "Address", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("integer");

                            b1.Property<string>("City")
                                .HasMaxLength(30)
                                .HasColumnType("character varying(30)")
                                .HasColumnName("AddressCity");

                            b1.Property<string>("Country")
                                .HasMaxLength(2)
                                .HasColumnType("character(2)")
                                .HasColumnName("AddressCountry")
                                .IsFixedLength();

                            b1.Property<string>("Line1")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("AddressLine1");

                            b1.Property<string>("Line2")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("AddressLine2");

                            b1.Property<string>("PostalCode")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("AddressPostalCode");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders", "OrM");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("ModularMonolith.Modules.Ordering.Domain.ContractorId", "ContractorId", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .HasColumnType("integer")
                                .HasColumnName("ContractorId");

                            b1.Property<string>("TypeId")
                                .IsRequired()
                                .HasMaxLength(5)
                                .IsUnicode(false)
                                .HasColumnType("character(5)")
                                .HasColumnName("ContractorTypeId")
                                .IsFixedLength();

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders", "OrM");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("ModularMonolith.Modules.Ordering.Domain.WarehouseId", "WarehouseId", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .HasColumnType("integer")
                                .HasColumnName("WarehouseId");

                            b1.Property<string>("TypeId")
                                .IsRequired()
                                .HasMaxLength(5)
                                .IsUnicode(false)
                                .HasColumnType("character(5)")
                                .HasColumnName("WarehouseTypeId")
                                .IsFixedLength();

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders", "OrM");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Address");

                    b.Navigation("ContractorId")
                        .IsRequired();

                    b.Navigation("WarehouseId")
                        .IsRequired();
                });

            modelBuilder.Entity("ModularMonolith.Modules.Ordering.Domain.Orders.OrderItem", b =>
                {
                    b.HasOne("ModularMonolith.Modules.Ordering.Domain.Orders.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ModularMonolith.Modules.Ordering.Domain.ArticleId", "ArticleId", b1 =>
                        {
                            b1.Property<int>("OrderItemId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .HasColumnType("integer")
                                .HasColumnName("ArticleId");

                            b1.Property<string>("TypeId")
                                .IsRequired()
                                .HasMaxLength(5)
                                .IsUnicode(false)
                                .HasColumnType("character(5)")
                                .HasColumnName("ArticleTypeId")
                                .IsFixedLength();

                            b1.HasKey("OrderItemId");

                            b1.ToTable("OrderItems", "OrM");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.OwnsOne("ModularMonolith.Modules.Ordering.Domain.Quantity", "Quantity", b1 =>
                        {
                            b1.Property<int>("OrderItemId")
                                .HasColumnType("integer");

                            b1.Property<int>("Denominator")
                                .HasColumnType("integer")
                                .HasColumnName("QuantityDenominator");

                            b1.Property<int>("Numerator")
                                .HasColumnType("integer")
                                .HasColumnName("QuantityNumerator");

                            b1.Property<string>("Unit")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("character varying(10)")
                                .HasColumnName("QuantityUnit");

                            b1.Property<decimal>("Value")
                                .HasPrecision(14, 4)
                                .HasColumnType("numeric(14,4)")
                                .HasColumnName("Quantity");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("OrderItems", "OrM");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.Navigation("ArticleId")
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Quantity")
                        .IsRequired();
                });

            modelBuilder.Entity("ModularMonolith.Modules.Ordering.Domain.Orders.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
