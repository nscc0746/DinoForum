using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DinoForum.Migrations
{
    /// <inheritdoc />
    public partial class IdentityModelsIntegration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DinoForumUserId",
                table: "Discussion",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DinoForumUserId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Discussion_DinoForumUserId",
                table: "Discussion",
                column: "DinoForumUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_DinoForumUserId",
                table: "Comment",
                column: "DinoForumUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_DinoForumUserId",
                table: "Comment",
                column: "DinoForumUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Discussion_AspNetUsers_DinoForumUserId",
                table: "Discussion",
                column: "DinoForumUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_DinoForumUserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Discussion_AspNetUsers_DinoForumUserId",
                table: "Discussion");

            migrationBuilder.DropIndex(
                name: "IX_Discussion_DinoForumUserId",
                table: "Discussion");

            migrationBuilder.DropIndex(
                name: "IX_Comment_DinoForumUserId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "DinoForumUserId",
                table: "Discussion");

            migrationBuilder.DropColumn(
                name: "DinoForumUserId",
                table: "Comment");
        }
    }
}
