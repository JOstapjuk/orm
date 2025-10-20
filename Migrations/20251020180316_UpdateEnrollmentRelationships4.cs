using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace orm.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEnrollmentRelationships4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Teachers_TeacherId",
                table: "Enrollments");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Teachers_TeacherId",
                table: "Enrollments",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Teachers_TeacherId",
                table: "Enrollments");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Enrollments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Teachers_TeacherId",
                table: "Enrollments",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
