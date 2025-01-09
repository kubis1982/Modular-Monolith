using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ModularMonolith.Modules.Warehouses.Persistance.WriteModel.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "WaM");

            migrationBuilder.CreateTable(
                name: "Warehouses",
                schema: "WaM",
                columns: table => new
                {
                    TypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false, defaultValue: "WaM01"),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: true),
                    Code = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "WaM",
                table: "Warehouses",
                columns: new[] { "Id", "Code", "CreatedBy", "Description", "IsBlocked", "ModifiedBy", "ModifiedOn", "Name", "TypeId" },
                values: new object[] { 1, "MAG", 1, null, false, null, null, "Magazyn domyślny", "WaM01" });

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_Code",
                schema: "WaM",
                table: "Warehouses",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Warehouses",
                schema: "WaM");
        }
    }
}
