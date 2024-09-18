using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class EntityV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingComments_RatingComments_ParentCommentId",
                table: "RatingComments");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingComments_Ratings_RatingId",
                table: "RatingComments");

            migrationBuilder.AddColumn<Guid>(
                name: "RatingId1",
                table: "RatingComments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RatingComments_RatingId1",
                table: "RatingComments",
                column: "RatingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_RatingComments_ParentCommentId",
                table: "RatingComments",
                column: "ParentCommentId",
                principalTable: "RatingComments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_Ratings_RatingId",
                table: "RatingComments",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_Ratings_RatingId1",
                table: "RatingComments",
                column: "RatingId1",
                principalTable: "Ratings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingComments_RatingComments_ParentCommentId",
                table: "RatingComments");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingComments_Ratings_RatingId",
                table: "RatingComments");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingComments_Ratings_RatingId1",
                table: "RatingComments");

            migrationBuilder.DropIndex(
                name: "IX_RatingComments_RatingId1",
                table: "RatingComments");

            migrationBuilder.DropColumn(
                name: "RatingId1",
                table: "RatingComments");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_RatingComments_ParentCommentId",
                table: "RatingComments",
                column: "ParentCommentId",
                principalTable: "RatingComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_Ratings_RatingId",
                table: "RatingComments",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
