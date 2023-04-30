using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Healthy.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CRM = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CPF = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DoctorId = table.Column<int>(type: "INTEGER", nullable: false),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Invoice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSpeciality",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecialityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpeciality", x => new { x.DoctorId, x.SpecialityId });
                    table.ForeignKey(
                        name: "FK_DoctorSpeciality_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSpeciality_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "BirthDate", "CRM", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, new DateTime(2003, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "PR792268", "gabrieldamke@alunos.utfpr.edu.br", "Gabriel", "Trevisan", "(45) 99134-5657" },
                    { 2, new DateTime(2002, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "PR25472", "marlonangeli@alunos.utfpr.edu.br", "Marlon", "Angeli", "(45) 98421-4127" },
                    { 3, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PR165557", "mateusstamm@alunos.utfpr.edu.br", "Mateus", "Stamm", "(55) 99931-5511" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "BirthDate", "CPF", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, new DateTime(1973, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "507.303.030-39", "artzao@healthy.com", "Arthur", "Henrique", "(11) 99123-4567" },
                    { 2, new DateTime(1983, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "561.317.130-06", "gabs@healthy.com", "Gabriel", "Stabile", "(45) 99876-5432" },
                    { 3, new DateTime(1990, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "771.708.640-96", "cheng@healthy.com", "Cheng", "Wei Chih", "(45) 99123-4567" },
                    { 4, new DateTime(1999, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "372.469.560-85", "guilherme@healthy.com", "Guilherme", "Augusto", "(45) 99876-5432" }
                });

            migrationBuilder.InsertData(
                table: "Specialities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Cardiologia" },
                    { 2, "Dermatologia" },
                    { 3, "Clínico Geral" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "CreatedAt", "Date", "Description", "DoctorId", "Invoice", "PatientId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 29, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7557), new DateTime(2023, 4, 30, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7549), "Consulta de rotina", 1, 54.2m, 1, 1 },
                    { 2, new DateTime(2023, 4, 29, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7560), new DateTime(2023, 5, 1, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7559), "Consulta de rotina", 1, 54.2m, 2, 1 },
                    { 3, new DateTime(2023, 4, 26, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7566), new DateTime(2023, 4, 27, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7562), "Checagem de exames", 2, 54.2m, 3, 3 },
                    { 4, new DateTime(2023, 4, 29, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7569), new DateTime(2023, 4, 29, 21, 48, 12, 132, DateTimeKind.Local).AddTicks(7568), "Operação", 3, 54.2m, 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "DoctorSpeciality",
                columns: new[] { "DoctorId", "SpecialityId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_CRM",
                table: "Doctors",
                column: "CRM",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpeciality_SpecialityId",
                table: "DoctorSpeciality",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CPF",
                table: "Patients",
                column: "CPF",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "DoctorSpeciality");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Specialities");
        }
    }
}
