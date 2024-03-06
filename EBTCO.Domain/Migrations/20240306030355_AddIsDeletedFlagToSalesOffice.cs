using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBTCO.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedFlagToSalesOffice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SalesOffices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SalesOffices");
        }
    }
}
