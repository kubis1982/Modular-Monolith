using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ModularMonolith.Modules.Orders.Persistance.WriteModel.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "OrM");

            migrationBuilder.CreateTable(
                name: "DocumentNumbers",
                schema: "OrM",
                columns: table => new
                {
                    EntityTypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    Month = table.Column<byte>(type: "smallint", nullable: false),
                    Day = table.Column<byte>(type: "smallint", nullable: false),
                    Numerator = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentNumbers", x => new { x.EntityTypeId, x.Year, x.Month, x.Day });
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "OrM",
                columns: table => new
                {
                    TypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: true),
                    Version = table.Column<Guid>(type: "uuid", nullable: true),
                    ContractorTypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    ContractorId = table.Column<int>(type: "integer", nullable: false),
                    WarehouseTypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: false),
                    Status = table.Column<byte>(type: "smallint", nullable: false),
                    OrderNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OrderType = table.Column<byte>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    AddressLine1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    AddressLine2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    AddressPostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    AddressCity = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    AddressCountry = table.Column<string>(type: "character(2)", fixedLength: true, maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "OrM",
                columns: table => new
                {
                    TypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp(2) with time zone", precision: 2, nullable: true),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: true),
                    Version = table.Column<Guid>(type: "uuid", nullable: true),
                    Position = table.Column<short>(type: "smallint", nullable: false),
                    ArticleTypeId = table.Column<string>(type: "character(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    ArticleId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    QuantityUnit = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    QuantityNumerator = table.Column<int>(type: "integer", nullable: false),
                    QuantityDenominator = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    QuantityCompleted = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "OrM",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Id_Position",
                schema: "OrM",
                table: "OrderItems",
                columns: new[] { "Id", "Position" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                schema: "OrM",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentNumbers",
                schema: "OrM");

            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "OrM");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "OrM");
        }
    }
}
