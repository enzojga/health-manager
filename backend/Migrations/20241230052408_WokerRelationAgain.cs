using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class WokerRelationAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Patients_PatientId",
                table: "Workers");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Workers",
                newName: "PatientAsNurseId");

            migrationBuilder.RenameIndex(
                name: "IX_Workers_PatientId",
                table: "Workers",
                newName: "IX_Workers_PatientAsNurseId");

            migrationBuilder.AddColumn<int>(
                name: "PatientAsDoctorId",
                table: "Workers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_PatientAsDoctorId",
                table: "Workers",
                column: "PatientAsDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Patients_PatientAsDoctorId",
                table: "Workers",
                column: "PatientAsDoctorId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Patients_PatientAsNurseId",
                table: "Workers",
                column: "PatientAsNurseId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Patients_PatientAsDoctorId",
                table: "Workers");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Patients_PatientAsNurseId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_PatientAsDoctorId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "PatientAsDoctorId",
                table: "Workers");

            migrationBuilder.RenameColumn(
                name: "PatientAsNurseId",
                table: "Workers",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Workers_PatientAsNurseId",
                table: "Workers",
                newName: "IX_Workers_PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Patients_PatientId",
                table: "Workers",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
