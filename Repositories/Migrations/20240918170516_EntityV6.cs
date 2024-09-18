using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class EntityV6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Pods_PodId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingComments_Ratings_RatingId1",
                table: "RatingComments");

            migrationBuilder.DropIndex(
                name: "IX_RatingComments_RatingId1",
                table: "RatingComments");

            migrationBuilder.DropColumn(
                name: "RatingId1",
                table: "RatingComments");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Services",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "BookingServices",
                newName: "TotalPrice");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Pods_PodId",
                table: "Bookings",
                column: "PodId",
                principalTable: "Pods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Pods_PodId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Services",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "BookingServices",
                newName: "UnitPrice");

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
                name: "FK_Bookings_Pods_PodId",
                table: "Bookings",
                column: "PodId",
                principalTable: "Pods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingComments_Ratings_RatingId1",
                table: "RatingComments",
                column: "RatingId1",
                principalTable: "Ratings",
                principalColumn: "Id");
        }
    }
}
