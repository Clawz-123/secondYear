using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace secondYear.Migrations
{
    /// <inheritdoc />
    public partial class TravelPackageDto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TravelId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TravelId",
                table: "Reviews",
                column: "TravelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_TravelPackages_TravelId",
                table: "Reviews",
                column: "TravelId",
                principalTable: "TravelPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_TravelPackages_TravelId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_TravelId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "TravelId",
                table: "Reviews");
        }
    }
}
