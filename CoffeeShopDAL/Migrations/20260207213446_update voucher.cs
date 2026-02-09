using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShopDAL.Migrations
{
    /// <inheritdoc />
    public partial class updatevoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Carts_CartId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_CartId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Vouchers");

            migrationBuilder.RenameColumn(
                name: "IsValid",
                table: "Vouchers",
                newName: "IsActive");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Vouchers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumCartTotal",
                table: "Vouchers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "VoucherId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_VoucherId",
                table: "Carts",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Vouchers_VoucherId",
                table: "Carts",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Vouchers_VoucherId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_VoucherId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "MinimumCartTotal",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Vouchers",
                newName: "IsValid");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Vouchers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Vouchers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_CartId",
                table: "Vouchers",
                column: "CartId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Carts_CartId",
                table: "Vouchers",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
