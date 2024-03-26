using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBTCO.DB.Migrations
{
    /// <inheritdoc />
    public partial class MakeOwningProgressAutoCaculate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OwningProgress",
                table: "Properties",
                type: "int",
                nullable: false,
                computedColumnSql: "dbo.SumOwningPerc(ID)",
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OwningProgress",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "dbo.SumOwningPerc(ID)");
        }
    }
}
