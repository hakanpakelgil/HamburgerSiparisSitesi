using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HamburgerMVC4.Data.Migrations
{
    /// <inheritdoc />
    public partial class eleventh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Extras");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Beef" },
                    { 2, "Chicken" },
                    { 3, "Kids" },
                    { 4, "Vegan" }
                });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "cocacola.png", "Coca Cola" },
                    { 2, "fanta.png", "Fanta" },
                    { 3, "ayran.png", "Ayran" },
                    { 4, "sprite.png", "Sprite" },
                    { 5, "cocacolazero.png", "Coca Cola Zero" },
                    { 6, "fusetealemon.png", "Fuse Tea Lemon" },
                    { 7, "fuseteapeach.png", "Fuse Tea Peach" }
                });

            migrationBuilder.InsertData(
                table: "Extras",
                columns: new[] { "Id", "Name", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Ketchup", 5.0 },
                    { 2, "Mayonaise", 5.0 },
                    { 3, "Mustard", 10.0 },
                    { 4, "Chipotle", 15.0 },
                    { 5, "Ranch", 10.0 },
                    { 6, "BBQ", 15.0 }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Name", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, "Two flame grilled 100% beef patties with melted American cheese, freshly cut \r\niceberg lettuce, sliced white onions, crunchy pickles and Big king sauce all \r\non a warm, toasted, sesame seed bun.", "bigking.png", "Big King", 185.0 },
                    { 2, 1, "The Boost Burger's Big Mac® is a 100% beef burger with a taste like no other. \r\nThe mouthwatering perfection starts with two 100% pure all beef patties and \r\nBig Mac® sauce sandwiched between a sesame seed bun. It's topped off with pickles, \r\ncrisp shredded lettuce, finely chopped onion, and a slice of American cheese.", "bigmac.png", "Big Mac", 195.0 },
                    { 3, 1, "This legendary American burger is a true all-rounder! A 100% beef patty, \r\nEmmental cheese, onions, juicy tomatoes and a unique smoky flavoured sauce \r\nall nested in a freshly toasted bun.", "bigtasty.png", "Big Tasty", 200.0 },
                    { 4, 1, "Cheeseburger is a hamburger with a slice of melted cheese on top of the \r\nmeat patty, added near the end of the cooking time. Cheeseburgers can include \r\nvariations in structure, ingredients and composition.\r\n", "cheeseburger.png", "Cheeseburger", 180.0 },
                    { 5, 2, "Tasty chicken wrapped in a special crisp coating, topped with iceberg lettuce, \r\ncreamy mayo and crowned with a toasted sesame seed bun.", "chickenroyale.png", "Chicken Royale", 165.0 },
                    { 6, 1, "The Boost Burger's Double Cheeseburger features two 100% pure all beef patties \r\nseasoned with just a pinch of salt and pepper. It's topped with tangy pickles, \r\nchopped onions, ketchup, mustard, and two melty American cheese slices.", "doublecheeseburger.png", "Double Cheeseburger", 185.0 },
                    { 7, 2, "McChicken® Sandwich. Crispy coated chicken with lettuce and our sandwich sauce, \r\nin a soft, sesame-topped bun. A true classic.\r\n", "mcchicken.png", "McChicken Sandwich", 150.0 },
                    { 8, 1, "The Whopper is a hamburger consisting of a flame-grilled 4 oz (110 g) beef patty, \r\nsesame seed bun, mayonnaise, lettuce, tomato, pickles, ketchup, and sliced onion. \r\nOptional ingredients such as American cheese, bacon, mustard, guacamole or \r\njalapeño peppers may be added upon request.", "whopper.png", "Whopper", 197.0 },
                    { 9, 3, "Charbroiled all-beef patty topped with dill pickles, ketchup and mustard on a \r\nplain bun. Served with kid's drink and kid's fry.", "happykids.png", "Happy Kids", 145.0 },
                    { 10, 4, "A juicy flame-grilled, 100% plant-based patty topped with creamy mayonnaise, \r\nfreshly cut iceberg lettuce, juicy tomatoes, sliced white onions, crunchy pickles, \r\nand ketchup on a toasted sesame seed bun.", "plantbasedwhopper.png", "Plant Based Whopper", 165.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Extras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
