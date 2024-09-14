using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModularMonolith.Modules.AccessManagement.Persistance.WriteModel.Migrations
{
    /// <inheritdoc />
    public partial class AddAdministratorAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "AcM",
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "Email", "IsActive", "ModifiedBy", "ModifiedOn", "Password", "TypeId", "FirstName", "LastName", "MiddleName" },
                values: new object[] { 1, 1, "administrator@kubis1982.com", true, null, null, "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", "AcM01", "", "Administrator", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "AcM",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
