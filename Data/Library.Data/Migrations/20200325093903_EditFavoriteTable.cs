namespace Library.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EditFavoriteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Favorites_FavoriteId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_FavoriteId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Favorites",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_BookId",
                table: "Favorites",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Books_BookId",
                table: "Favorites",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Books_BookId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_BookId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Favorites");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_FavoriteId",
                table: "Books",
                column: "FavoriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Favorites_FavoriteId",
                table: "Books",
                column: "FavoriteId",
                principalTable: "Favorites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
