using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMaze.Migrations
{
    public partial class AddSchoolStaffFkSchool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SchoolId",
                table: "school_staff",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_school_staff_SchoolId",
                table: "school_staff",
                column: "SchoolId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_school_staff_school_building_SchoolId",
                table: "school_staff",
                column: "SchoolId",
                principalTable: "school_building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_school_staff_school_building_SchoolId",
                table: "school_staff");

            migrationBuilder.DropIndex(
                name: "IX_school_staff_SchoolId",
                table: "school_staff");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "school_staff");
        }
    }
}
