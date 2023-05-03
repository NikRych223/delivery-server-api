using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace delivery_server_api.Migrations
{
    /// <inheritdoc />
    public partial class threesMigr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteItem_AspNetUsers_UserId",
                table: "FavoriteItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteItem",
                table: "FavoriteItem");

            migrationBuilder.RenameTable(
                name: "FavoriteItem",
                newName: "Favorite");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteItem_UserId",
                table: "Favorite",
                newName: "IX_Favorite_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_AspNetUsers_UserId",
                table: "Favorite",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_AspNetUsers_UserId",
                table: "Favorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite");

            migrationBuilder.RenameTable(
                name: "Favorite",
                newName: "FavoriteItem");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_UserId",
                table: "FavoriteItem",
                newName: "IX_FavoriteItem_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteItem",
                table: "FavoriteItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteItem_AspNetUsers_UserId",
                table: "FavoriteItem",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
