using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBTCO.DB.Migrations
{
    /// <inheritdoc />
    public partial class CacheSomeInfoInSalesOffices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagerName",
                table: "SalesOffices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NoOfProperty",
                table: "SalesOffices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerID",
                table: "PropOwners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "OwningProgress",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PropOwners_OwnerID",
                table: "PropOwners",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_PropOwners_Owners_OwnerID",
                table: "PropOwners",
                column: "OwnerID",
                principalTable: "Owners",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropOwners_Owners_OwnerID",
                table: "PropOwners");

            migrationBuilder.DropIndex(
                name: "IX_PropOwners_OwnerID",
                table: "PropOwners");

            migrationBuilder.DropColumn(
                name: "ManagerName",
                table: "SalesOffices");

            migrationBuilder.DropColumn(
                name: "NoOfProperty",
                table: "SalesOffices");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "PropOwners");

            migrationBuilder.DropColumn(
                name: "OwningProgress",
                table: "Properties");
        }
    }
}
