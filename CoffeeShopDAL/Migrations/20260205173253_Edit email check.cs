using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShopDAL.Migrations
{
    /// <inheritdoc />
    public partial class Editemailcheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "EmailCheck",
                table: "AspNetUsers");

            migrationBuilder.AddCheckConstraint(
                name: "EmailCheck",
                table: "AspNetUsers",
                sql: "Email Like '_%@_%._%'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "EmailCheck",
                table: "AspNetUsers");

            migrationBuilder.AddCheckConstraint(
                name: "EmailCheck",
                table: "AspNetUsers",
                sql: "Email Like '   _%@_%._%'");
        }
    }
}
