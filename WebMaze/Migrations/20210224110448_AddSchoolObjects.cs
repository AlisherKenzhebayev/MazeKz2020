using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMaze.Migrations
{
    public partial class AddSchoolObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "school_building",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<long>(type: "bigint", nullable: false),
                    SchoolName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_school_building", x => x.Id);
                    table.ForeignKey(
                        name: "FK_school_building_Adress_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Adress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "school_certificate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenUserId = table.Column<long>(type: "bigint", nullable: false),
                    CertificateId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_school_certificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_school_certificate_Certificates_CertificateId",
                        column: x => x.CertificateId,
                        principalTable: "Certificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_school_certificate_CitizenUser_CitizenUserId",
                        column: x => x.CitizenUserId,
                        principalTable: "CitizenUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "school_staff",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenUserId = table.Column<long>(type: "bigint", nullable: false),
                    StaffMember = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_school_staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_school_staff_CitizenUser_CitizenUserId",
                        column: x => x.CitizenUserId,
                        principalTable: "CitizenUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "school_student",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenUserId = table.Column<long>(type: "bigint", nullable: false),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EducationStatus = table.Column<int>(type: "int", nullable: false),
                    GraduationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Major = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_school_student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_school_student_CitizenUser_CitizenUserId",
                        column: x => x.CitizenUserId,
                        principalTable: "CitizenUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_school_student_school_building_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "school_building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "school_subject",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false),
                    OfficeNumber = table.Column<long>(type: "bigint", nullable: true),
                    SubjectCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelevantMajor = table.Column<int>(type: "int", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_school_subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_school_subject_school_building_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "school_building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "school_schedule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenUserId = table.Column<long>(type: "bigint", nullable: false),
                    SubjectId = table.Column<long>(type: "bigint", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartHoursMinutes = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndHoursMinutes = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_school_schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_school_schedule_CitizenUser_CitizenUserId",
                        column: x => x.CitizenUserId,
                        principalTable: "CitizenUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_school_schedule_school_subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "school_subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_school_building_AddressId",
                table: "school_building",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_school_certificate_CertificateId",
                table: "school_certificate",
                column: "CertificateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_school_certificate_CitizenUserId",
                table: "school_certificate",
                column: "CitizenUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_school_schedule_CitizenUserId",
                table: "school_schedule",
                column: "CitizenUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_school_schedule_SubjectId",
                table: "school_schedule",
                column: "SubjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_school_staff_CitizenUserId",
                table: "school_staff",
                column: "CitizenUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_school_student_CitizenUserId",
                table: "school_student",
                column: "CitizenUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_school_student_SchoolId",
                table: "school_student",
                column: "SchoolId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_school_subject_SchoolId",
                table: "school_subject",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_school_subject_SubjectCode",
                table: "school_subject",
                column: "SubjectCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "school_certificate");

            migrationBuilder.DropTable(
                name: "school_schedule");

            migrationBuilder.DropTable(
                name: "school_staff");

            migrationBuilder.DropTable(
                name: "school_student");

            migrationBuilder.DropTable(
                name: "school_subject");

            migrationBuilder.DropTable(
                name: "school_building");
        }
    }
}
