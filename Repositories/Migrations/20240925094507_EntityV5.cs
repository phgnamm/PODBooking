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
                name: "FK_RatingComments_AspNetUsers_AccountId",
                table: "RatingComments");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingComments_Ratings_RatingId",
                table: "RatingComments");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_AspNetUsers_AccountId",
                table: "RatingComments",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_Ratings_RatingId",
                table: "RatingComments",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingComments_AspNetUsers_AccountId",
                table: "RatingComments");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingComments_Ratings_RatingId",
                table: "RatingComments");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_AspNetUsers_AccountId",
                table: "RatingComments",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_Ratings_RatingId",
                table: "RatingComments",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
