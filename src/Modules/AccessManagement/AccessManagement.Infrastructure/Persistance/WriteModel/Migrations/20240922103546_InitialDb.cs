﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

#nullable disable

namespace ModularMonolith.Modules.AccessManagement.Persistance.WriteModel.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AcM");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "AcM",
                columns: table => new
                {
                    TypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false, defaultValue: "AcM01"),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: true),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Token = table.Column<Guid>(type: "uuid", nullable: true),
                    TokenExpirationDate = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                schema: "AcM",
                columns: table => new
                {
                    TypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false, defaultValue: "AcM02"),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: false),
                    RefreshToken = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RefreshTokenExpirationDate = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true),
                    KilledBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "AcM",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_KilledBy",
                        column: x => x.KilledBy,
                        principalSchema: "AcM",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "AcM",
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "Email", "IsActive", "ModifiedBy", "ModifiedOn", "Password", "TypeId", "FirstName", "LastName", "MiddleName" },
                values: new object[] { 1, 1, "administrator@kubis1982.com", true, null, null, "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", "AcM01", "", "Administrator", "" });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CreatedBy",
                schema: "AcM",
                table: "Sessions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_KilledBy",
                schema: "AcM",
                table: "Sessions",
                column: "KilledBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "AcM",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Token",
                schema: "AcM",
                table: "Users",
                column: "Token",
                unique: true)
                .Annotation("Npgsql:NullsDistinct", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions",
                schema: "AcM");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "AcM");
        }
    }
}
