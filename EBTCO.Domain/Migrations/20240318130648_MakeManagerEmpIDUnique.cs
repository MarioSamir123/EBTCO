using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBTCO.DB.Migrations
{
    /// <inheritdoc />
    public partial class MakeManagerEmpIDUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SalesOffices_ManagerEmpID",
                table: "SalesOffices",
                column: "ManagerEmpID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SalesOffices_ManagerEmpID",
                table: "SalesOffices");
        }
    }
}
