using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBTCO.DB.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Properties",
                newName: "PriceTo");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceFrom",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceFrom",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "PriceTo",
                table: "Properties",
                newName: "Price");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Properties",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
