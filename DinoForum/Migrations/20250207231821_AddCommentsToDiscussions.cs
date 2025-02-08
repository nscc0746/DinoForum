using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DinoForum.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentsToDiscussions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Comment_DiscussionId",
                table: "Comment",
                column: "DiscussionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Discussion_DiscussionId",
                table: "Comment",
                column: "DiscussionId",
                principalTable: "Discussion",
                principalColumn: "DiscussionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Discussion_DiscussionId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_DiscussionId",
                table: "Comment");
        }
    }
}
