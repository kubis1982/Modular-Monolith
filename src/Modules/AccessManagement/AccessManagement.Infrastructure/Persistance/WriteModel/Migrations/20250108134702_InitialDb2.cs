using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace ModularMonolith.Modules.AccessManagement.Persistance.WriteModel.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                schema: "AcM",
                table: "Sessions",
                type: "timestamp(2) with time zone",
                precision: 2,
                nullable: true,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp(2) with time zone",
                oldPrecision: 2,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                schema: "AcM",
                table: "Sessions",
                type: "timestamp(2) with time zone",
                precision: 2,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(2) with time zone",
                oldPrecision: 2,
                oldNullable: true,
                oldDefaultValueSql: "NOW()");
        }
    }
}
