using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pri.Ca.Infrastructure.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Rpg" },
                    { 2, "Sports" },
                    { 3, "Fantasy" },
                    { 4, "Adventure" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Square Enix" },
                    { 2, "EA" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "PublisherId", "Title" },
                values: new object[] { 1, "Rpg classic", 1, "Final Fantasy" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "PublisherId", "Title" },
                values: new object[] { 2, "Cool soccer game", 2, "Fifa20" });

            migrationBuilder.InsertData(
                table: "GameGenre",
                columns: new[] { "GamesId", "GenresId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 2 },
                    { 2, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameGenre",
                keyColumns: new[] { "GamesId", "GenresId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "GameGenre",
                keyColumns: new[] { "GamesId", "GenresId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "GameGenre",
                keyColumns: new[] { "GamesId", "GenresId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "GameGenre",
                keyColumns: new[] { "GamesId", "GenresId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "GameGenre",
                keyColumns: new[] { "GamesId", "GenresId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
