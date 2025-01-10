using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ModularMonolith.Modules.Articles.Persistance.WriteModel.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ArM");

            migrationBuilder.CreateTable(
                name: "Articles",
                schema: "ArM",
                columns: table => new
                {
                    TypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false, defaultValue: "ArM01"),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: true),
                    Code = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Unit = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementUnits",
                schema: "ArM",
                columns: table => new
                {
                    TypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false, defaultValue: "ArM02"),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: true),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementUnits", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "ArM",
                table: "MeasurementUnits",
                columns: new[] { "Id", "Code", "CreatedBy", "ModifiedBy", "ModifiedOn", "Name", "TypeId" },
                values: new object[] { 1, "kg", 1, null, null, "kilogram", "ArM02" });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Code",
                schema: "ArM",
                table: "Articles",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementUnits_Code",
                schema: "ArM",
                table: "MeasurementUnits",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles",
                schema: "ArM");

            migrationBuilder.DropTable(
                name: "MeasurementUnits",
                schema: "ArM");
        }
    }
}
