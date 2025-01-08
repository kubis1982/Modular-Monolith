﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModularMonolith.Modules.AccessManagement.Persistance.WriteModel;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ModularMonolith.Modules.AccessManagement.Persistance.WriteModel.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    partial class WriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("AcM")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ModularMonolith.Modules.AccessManagement.Domain.Users.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnOrder(3);

                    b.Property<DateTime?>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(2)
                        .HasColumnType("timestamp(2) with time zone")
                        .HasColumnOrder(2)
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime>("ExpirationDate")
                        .HasPrecision(2)
                        .HasColumnType("timestamp(2) with time zone");

                    b.Property<int?>("KilledBy")
                        .HasColumnType("integer");

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
                        .HasDefaultValue("AcM02")
                        .HasColumnOrder(0)
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("KilledBy");

                    b.ToTable("Sessions", "AcM");
                });

            modelBuilder.Entity("ModularMonolith.Modules.AccessManagement.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnOrder(3);

                    b.Property<DateTime?>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(2)
                        .HasColumnType("timestamp(2) with time zone")
                        .HasColumnOrder(2)
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnOrder(7);

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("integer")
                        .HasColumnOrder(5);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasPrecision(2)
                        .HasColumnType("timestamp(2) with time zone")
                        .HasColumnOrder(4);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnOrder(8);

                    b.Property<string>("TypeId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("character(5)")
                        .HasDefaultValue("AcM01")
                        .HasColumnOrder(0)
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", "AcM");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = 1,
                            Email = "administrator@kubis1982.com",
                            IsActive = true,
                            Password = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                            TypeId = "AcM01"
                        });
                });

            modelBuilder.Entity("ModularMonolith.Modules.AccessManagement.Domain.Users.Session", b =>
                {
                    b.HasOne("ModularMonolith.Modules.AccessManagement.Domain.Users.User", null)
                        .WithMany("Sessions")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ModularMonolith.Modules.AccessManagement.Domain.Users.User", "Killer")
                        .WithMany()
                        .HasForeignKey("KilledBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsOne("ModularMonolith.Modules.AccessManagement.Domain.Users.RefreshToken", "RefreshToken", b1 =>
                        {
                            b1.Property<int>("SessionId")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("ExpirationDate")
                                .HasPrecision(2)
                                .HasColumnType("timestamp(2) with time zone")
                                .HasColumnName("RefreshTokenExpirationDate");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("character varying(256)")
                                .HasColumnName("RefreshToken");

                            b1.HasKey("SessionId");

                            b1.ToTable("Sessions", "AcM");

                            b1.WithOwner()
                                .HasForeignKey("SessionId");
                        });

                    b.Navigation("Killer");

                    b.Navigation("RefreshToken");
                });

            modelBuilder.Entity("ModularMonolith.Modules.AccessManagement.Domain.Users.User", b =>
                {
                    b.OwnsOne("ModularMonolith.Modules.AccessManagement.Domain.Users.UserFullName", "FullName", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("integer");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("FirstName")
                                .HasColumnOrder(10);

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("LastName")
                                .HasColumnOrder(12);

                            b1.Property<string>("MiddleName")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("MiddleName")
                                .HasColumnOrder(11);

                            b1.HasKey("UserId");

                            b1.ToTable("Users", "AcM");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.HasData(
                                new
                                {
                                    UserId = 1,
                                    FirstName = "",
                                    LastName = "Administrator",
                                    MiddleName = ""
                                });
                        });

                    b.OwnsOne("ModularMonolith.Modules.AccessManagement.Domain.Users.UserToken", "Token", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("ExpirationDate")
                                .HasPrecision(2)
                                .HasColumnType("timestamp(2) with time zone")
                                .HasColumnName("TokenExpirationDate");

                            b1.Property<Guid>("Token")
                                .HasColumnType("uuid")
                                .HasColumnName("Token");

                            b1.HasKey("UserId");

                            b1.HasIndex("Token")
                                .IsUnique();

                            NpgsqlIndexBuilderExtensions.AreNullsDistinct(b1.HasIndex("Token"), true);

                            b1.ToTable("Users", "AcM");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("FullName")
                        .IsRequired();

                    b.Navigation("Token");
                });

            modelBuilder.Entity("ModularMonolith.Modules.AccessManagement.Domain.Users.User", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
