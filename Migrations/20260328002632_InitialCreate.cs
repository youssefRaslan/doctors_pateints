using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doctors.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientDoctors",
                table: "PatientDoctors");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "patients",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "patients",
                newName: "Role");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "patients",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "patients",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PatientDoctors",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PatientDoctors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PatientDoctors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientDoctors",
                table: "PatientDoctors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DoctorPatientRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorPatientRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorPatientRequests_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorPatientRequests_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    SugarLevel = table.Column<double>(type: "float", nullable: false),
                    BloodPressure = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurements_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientDoctors_PatientId",
                table: "PatientDoctors",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatientRequests_DoctorId",
                table: "DoctorPatientRequests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatientRequests_PatientId",
                table: "DoctorPatientRequests",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_PatientId",
                table: "Measurements",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorPatientRequests");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientDoctors",
                table: "PatientDoctors");

            migrationBuilder.DropIndex(
                name: "IX_PatientDoctors_PatientId",
                table: "PatientDoctors");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PatientDoctors");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PatientDoctors");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PatientDoctors");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "doctors");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "patients",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "patients",
                newName: "Image");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientDoctors",
                table: "PatientDoctors",
                columns: new[] { "PatientId", "DoctorId" });
        }
    }
}
