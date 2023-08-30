using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace printing_calculator.Migrations
{
    public partial class AddStapleBrochure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StapleBrochure",
                table: "InputsHistories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StapleBrochurePrice",
                table: "Histories",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StapleBrochure",
                table: "InputsHistories");

            migrationBuilder.DropColumn(
                name: "StapleBrochurePrice",
                table: "Histories");
        }
    }
}
