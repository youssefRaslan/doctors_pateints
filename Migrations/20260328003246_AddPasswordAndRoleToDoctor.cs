using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doctors.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordAndRoleToDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                defaultValue: "Doctor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "doctors");
        }
    }
}
