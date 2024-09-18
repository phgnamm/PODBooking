using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class EntityV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingComments_RatingComments_ParentCommentId",
                table: "RatingComments");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_RatingComments_ParentCommentId",
                table: "RatingComments",
                column: "ParentCommentId",
                principalTable: "RatingComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingComments_RatingComments_ParentCommentId",
                table: "RatingComments");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_RatingComments_ParentCommentId",
                table: "RatingComments",
                column: "ParentCommentId",
                principalTable: "RatingComments",
                principalColumn: "Id");
        }
    }
}
