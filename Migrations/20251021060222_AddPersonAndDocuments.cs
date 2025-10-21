using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace orm.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonAndDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_ContactDatas_ContactDataId",
                table: "Authors");

            migrationBuilder.DropTable(
                name: "ContactDatas");

            migrationBuilder.DropIndex(
                name: "IX_Authors_ContactDataId",
                table: "Authors");

            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ContactId",
                table: "Authors",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_ContactInfos_ContactId",
                table: "Authors",
                column: "ContactId",
                principalTable: "ContactInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_ContactInfos_ContactId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_ContactId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Authors");

            migrationBuilder.CreateTable(
                name: "ContactDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDatas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ContactDataId",
                table: "Authors",
                column: "ContactDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_ContactDatas_ContactDataId",
                table: "Authors",
                column: "ContactDataId",
                principalTable: "ContactDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
