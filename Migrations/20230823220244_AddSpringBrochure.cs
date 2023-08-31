using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace printing_calculator.Migrations
{
    public partial class AddSpringBrochure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PaperThickness",
                table: "PaperCatalogs",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "SpringBrochureSettingId",
                table: "Markups",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpringBrochure",
                table: "InputsHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpringBrochurePrice",
                table: "Histories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpringBrochureSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CoverCardboardA4Price = table.Column<int>(type: "integer", nullable: false),
                    CoverCardboardA3Price = table.Column<int>(type: "integer", nullable: false),
                    CoverPlasticA4Price = table.Column<int>(type: "integer", nullable: false),
                    CoverPlasticA3Price = table.Column<int>(type: "integer", nullable: false),
                    PriceForA3 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpringBrochureSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Markups_SpringBrochureSettingId",
                table: "Markups",
                column: "SpringBrochureSettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Markups_SpringBrochureSettings_SpringBrochureSettingId",
                table: "Markups",
                column: "SpringBrochureSettingId",
                principalTable: "SpringBrochureSettings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markups_SpringBrochureSettings_SpringBrochureSettingId",
                table: "Markups");

            migrationBuilder.DropTable(
                name: "SpringBrochureSettings");

            migrationBuilder.DropIndex(
                name: "IX_Markups_SpringBrochureSettingId",
                table: "Markups");

            migrationBuilder.DropColumn(
                name: "PaperThickness",
                table: "PaperCatalogs");

            migrationBuilder.DropColumn(
                name: "SpringBrochureSettingId",
                table: "Markups");

            migrationBuilder.DropColumn(
                name: "SpringBrochure",
                table: "InputsHistories");

            migrationBuilder.DropColumn(
                name: "SpringBrochurePrice",
                table: "Histories");
        }
    }
}
