using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace printing_calculator.Migrations
{
    public partial class add_comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "Histories",
                newName: "DateTime");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Histories",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Histories");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Histories",
                newName: "dateTime");
        }
    }
}
