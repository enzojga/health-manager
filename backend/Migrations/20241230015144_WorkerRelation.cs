using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class WorkerRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Workers_DoctorId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Workers_NurseId",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Workers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_PatientId",
                table: "Workers",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Workers_DoctorId",
                table: "Patients",
                column: "DoctorId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Workers_NurseId",
                table: "Patients",
                column: "NurseId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Patients_PatientId",
                table: "Workers",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Workers_DoctorId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Workers_NurseId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Patients_PatientId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_PatientId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Workers");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Workers_DoctorId",
                table: "Patients",
                column: "DoctorId",
                principalTable: "Workers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Workers_NurseId",
                table: "Patients",
                column: "NurseId",
                principalTable: "Workers",
                principalColumn: "Id");
        }
    }
}
