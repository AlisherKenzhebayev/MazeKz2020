using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMaze.Migrations
{
    public partial class removeFkSchoolBuilding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_school_building_Adress_AddressId",
                table: "school_building");

            migrationBuilder.DropIndex(
                name: "IX_school_building_AddressId",
                table: "school_building");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "school_building");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AddressId",
                table: "school_building",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_school_building_AddressId",
                table: "school_building",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_school_building_Adress_AddressId",
                table: "school_building",
                column: "AddressId",
                principalTable: "Adress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
