using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class EntityV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingComments_Ratings_RatingId",
                table: "RatingComments");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_Ratings_RatingId",
                table: "RatingComments",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingComments_Ratings_RatingId",
                table: "RatingComments");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_Ratings_RatingId",
                table: "RatingComments",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id");
        }
    }
}
