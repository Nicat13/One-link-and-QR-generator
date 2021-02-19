using Microsoft.EntityFrameworkCore.Migrations;

namespace OneLinkandQRGeneratorbyNijat.Migrations
{
    public partial class codeadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "uRLs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "uRLs");
        }
    }
}
