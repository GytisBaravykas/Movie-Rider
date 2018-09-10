using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieRider.Migrations
{
    public partial class ChangedRentalProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Movies_MoviesId",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "Rentals",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_MoviesId",
                table: "Rentals",
                newName: "IX_Rentals_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Movies_MovieId",
                table: "Rentals",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Movies_MovieId",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Rentals",
                newName: "MoviesId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_MovieId",
                table: "Rentals",
                newName: "IX_Rentals_MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Movies_MoviesId",
                table: "Rentals",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
