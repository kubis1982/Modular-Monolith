﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModularMonolith.Modules.Contractors.Persistance.WriteModel;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ModularMonolith.Modules.Contractors.Persistance.WriteModel.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    [Migration("20250109084134_InitialDb")]
    partial class InitialDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("CnM")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ModularMonolith.Modules.Contractors.Domain.Contractors.Contractor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnOrder(10);

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnOrder(3);

                    b.Property<DateTime?>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(2)
                        .HasColumnType("timestamp(2) with time zone")
                        .HasColumnOrder(2)
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(12);

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean")
                        .HasColumnOrder(13);

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("integer")
                        .HasColumnOrder(5);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasPrecision(2)
                        .HasColumnType("timestamp(2) with time zone")
                        .HasColumnOrder(4);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)")
                        .HasColumnOrder(11);

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("character(5)")
                        .HasDefaultValue("CnM01")
                        .HasColumnOrder(0)
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Contractors", "CnM");
                });

            modelBuilder.Entity("ModularMonolith.Modules.Contractors.Domain.Contractors.ContractorAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ContractorId")
                        .HasColumnType("integer")
                        .HasColumnOrder(11);

                    b.Property<string>("ContractorTypeId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("character(5)")
                        .HasDefaultValue("CnM01")
                        .HasColumnOrder(10)
                        .IsFixedLength();

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnOrder(3);

                    b.Property<DateTime?>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(2)
                        .HasColumnType("timestamp(2) with time zone")
                        .HasColumnOrder(2)
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("integer")
                        .HasColumnOrder(5);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasPrecision(2)
                        .HasColumnType("timestamp(2) with time zone")
                        .HasColumnOrder(4);

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("character(5)")
                        .HasDefaultValue("CnM02")
                        .HasColumnOrder(0)
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("ContractorId");

                    b.ToTable("Addresses", "CnM");
                });

            modelBuilder.Entity("ModularMonolith.Modules.Contractors.Domain.Contractors.Contractor", b =>
                {
                    b.OwnsOne("ModularMonolith.Modules.Contractors.Domain.Contractors.Country", "Country", b1 =>
                        {
                            b1.Property<int>("ContractorId")
                                .HasColumnType("integer");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2)
                                .HasColumnType("character varying(2)")
                                .HasColumnName("Country");

                            b1.HasKey("ContractorId");

                            b1.ToTable("Contractors", "CnM");

                            b1.WithOwner()
                                .HasForeignKey("ContractorId");
                        });

                    b.Navigation("Country")
                        .IsRequired();
                });

            modelBuilder.Entity("ModularMonolith.Modules.Contractors.Domain.Contractors.ContractorAddress", b =>
                {
                    b.HasOne("ModularMonolith.Modules.Contractors.Domain.Contractors.Contractor", null)
                        .WithMany("Addresses")
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ModularMonolith.Modules.Contractors.Domain.Contractors.Address", "Address", b1 =>
                        {
                            b1.Property<int>("ContractorAddressId")
                                .HasColumnType("integer");

                            b1.Property<string>("Line1")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("Line1");

                            b1.Property<string>("Line2")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("Line2");

                            b1.Property<string>("PostalCode")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("PostalCode");

                            b1.HasKey("ContractorAddressId");

                            b1.ToTable("Addresses", "CnM");

                            b1.WithOwner()
                                .HasForeignKey("ContractorAddressId");

                            b1.OwnsOne("ModularMonolith.Modules.Contractors.Domain.Contractors.Country", "Country", b2 =>
                                {
                                    b2.Property<int>("AddressContractorAddressId")
                                        .HasColumnType("integer");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(2)
                                        .HasColumnType("character varying(2)")
                                        .HasColumnName("Country");

                                    b2.HasKey("AddressContractorAddressId");

                                    b2.ToTable("Addresses", "CnM");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressContractorAddressId");
                                });

                            b1.OwnsOne("ModularMonolith.Modules.Contractors.Domain.Contractors.City", "City", b2 =>
                                {
                                    b2.Property<int>("AddressContractorAddressId")
                                        .HasColumnType("integer");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(30)
                                        .HasColumnType("character varying(30)")
                                        .HasColumnName("City");

                                    b2.HasKey("AddressContractorAddressId");

                                    b2.ToTable("Addresses", "CnM");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressContractorAddressId");
                                });

                            b1.Navigation("City")
                                .IsRequired();

                            b1.Navigation("Country")
                                .IsRequired();
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("ModularMonolith.Modules.Contractors.Domain.Contractors.Contractor", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
