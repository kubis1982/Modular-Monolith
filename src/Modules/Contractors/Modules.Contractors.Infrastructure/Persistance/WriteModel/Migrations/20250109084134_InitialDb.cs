using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ModularMonolith.Modules.Contractors.Persistance.WriteModel.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CnM");

            migrationBuilder.CreateTable(
                name: "Contractors",
                schema: "CnM",
                columns: table => new
                {
                    TypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false, defaultValue: "CnM01"),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: true),
                    Code = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    Country = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "CnM",
                columns: table => new
                {
                    TypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false, defaultValue: "CnM02"),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: true),
                    ContractorTypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: true, defaultValue: "CnM01"),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    Line1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Line2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    City = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Country = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalSchema: "CnM",
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ContractorId",
                schema: "CnM",
                table: "Addresses",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_Code",
                schema: "CnM",
                table: "Contractors",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "CnM");

            migrationBuilder.DropTable(
                name: "Contractors",
                schema: "CnM");
        }
    }
}
