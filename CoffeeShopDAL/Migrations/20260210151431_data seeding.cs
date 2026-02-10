using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoffeeShopDAL.Migrations
{
    /// <inheritdoc />
    public partial class dataseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsageLimit",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsedCount",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { 7, "coffee.jpg", "Coffee" },
                    { 8, "tea.jpg", "Tea" },
                    { 9, "dessert.jpg", "Dessert" },
                    { 10, "bakery.jpg", "Bakery" },
                    { 11, "drinks.jpg", "Drinks" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Name", "Price", "ProductSize", "Quantity" },
                values: new object[,]
                {
                    { 20, 7, "Strong black coffee", "espresso.jpg", "Espresso", 50m, 1, null },
                    { 21, 7, "Coffee with milk", "latte.jpg", "Latte", 120m, 2, null },
                    { 22, 7, "Coffee with foam", "cappuccino.jpg", "Cappuccino", 130m, 2, null },
                    { 23, 7, "Diluted espresso", "americano.jpg", "Americano", 90m, 1, null },
                    { 24, 7, "Chocolate coffee", "mocha.jpg", "Mocha", 100m, 3, null },
                    { 25, 8, "Healthy green tea", "green_tea.jpg", "Green Tea", 50m, 1, null },
                    { 26, 8, "Classic black tea", "black_tea.jpg", "Black Tea", 30m, 2, null },
                    { 27, 8, "Relaxing herbal tea", "chamomile_tea.jpg", "Chamomile Tea", 60m, 2, null },
                    { 28, 8, "Refreshing mint tea", "mint_tea.jpg", "Mint Tea", 40m, 1, null },
                    { 29, 8, "Flavored black tea", "earl_grey.jpg", "Earl Grey", 70m, 3, null },
                    { 30, 9, "Creamy cheesecake", "cheesecake.jpg", "Cheesecake", 250m, 2, 100 },
                    { 31, 9, "Rich chocolate brownie", "brownie.jpg", "Chocolate Brownie", 150m, 1, 20 },
                    { 32, 9, "French macaron", "macaron.jpg", "Macaron", 120m, 1, 300 },
                    { 33, 9, "Italian dessert", "tiramisu.jpg", "Tiramisu", 280m, 3, 80 },
                    { 34, 9, "Vanilla cupcake", "cupcake.jpg", "Cupcake", 100m, 1, 250 },
                    { 35, 10, "French bread", "baguette.jpg", "Baguette", 50m, 3, 500 },
                    { 36, 10, "Buttery croissant", "croissant.jpg", "Croissant", 80m, 2, 300 },
                    { 37, 10, "Sweet donut", "donut.jpg", "Donut", 150m, 1, 400 },
                    { 38, 10, "Blueberry muffin", "muffin.jpg", "Muffin", 70m, 1, 200 },
                    { 39, 10, "Soft bread roll", "bread_roll.jpg", "Bread Roll", 40m, 2, 350 },
                    { 40, 11, "Freshly squeezed", "orange_juice.jpg", "Orange Juice", 100m, 2, null },
                    { 41, 11, "Refreshing lemonade", "lemonade.jpg", "Lemonade", 80m, 2, null },
                    { 42, 11, "Sting", "sting.jpg", "Coca Cola", 60m, 3, null },
                    { 43, 11, "Orange soda", "fanta.jpg", "Fanta", 60m, 3, null },
                    { 44, 11, "Kiwi Juice", "kiwi_juice.jpg", "Mineral Water", 150m, 1, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "UsageLimit",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "UsedCount",
                table: "Vouchers");
        }
    }
}
