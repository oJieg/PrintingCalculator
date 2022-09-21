using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace printing_calculator.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumablePrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TonerPrice = table.Column<int>(type: "integer", nullable: false),
                    DrumPrice1 = table.Column<int>(type: "integer", nullable: false),
                    DrumPrice2 = table.Column<int>(type: "integer", nullable: false),
                    DrumPrice3 = table.Column<int>(type: "integer", nullable: false),
                    DrumPrice4 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumablePrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Laminations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laminations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SizePapers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameSizePaper = table.Column<string>(type: "text", nullable: false),
                    SizePaperHeight = table.Column<int>(type: "integer", nullable: false),
                    SizePaperWidth = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizePapers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LaminationPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<float>(type: "real", nullable: false),
                    LaminationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaminationPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaminationPrices_Laminations_LaminationId",
                        column: x => x.LaminationId,
                        principalTable: "Laminations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaperCatalogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SizeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaperCatalogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaperCatalogs_SizePapers_SizeId",
                        column: x => x.SizeId,
                        principalTable: "SizePapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryInputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Whidth = table.Column<int>(type: "integer", nullable: false),
                    PaperId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Kinds = table.Column<int>(type: "integer", nullable: false),
                    Duplex = table.Column<bool>(type: "boolean", nullable: false),
                    LaminationId = table.Column<int>(type: "integer", nullable: true),
                    CreasingAmount = table.Column<int>(type: "integer", nullable: false),
                    DrillingAmount = table.Column<int>(type: "integer", nullable: false),
                    RoundingAmount = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryInputs_Laminations_LaminationId",
                        column: x => x.LaminationId,
                        principalTable: "Laminations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoryInputs_PaperCatalogs_PaperId",
                        column: x => x.PaperId,
                        principalTable: "PaperCatalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PricePapers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<float>(type: "real", nullable: false),
                    CatalogId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricePapers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricePapers_PaperCatalogs_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "PaperCatalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Historys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InputId = table.Column<int>(type: "integer", nullable: false),
                    PricePaperId = table.Column<int>(type: "integer", nullable: false),
                    ConsumablePriceId = table.Column<int>(type: "integer", nullable: false),
                    MarkupPaper = table.Column<int>(type: "integer", nullable: true),
                    CutPrice = table.Column<int>(type: "integer", nullable: true),
                    LaminationPricesId = table.Column<int>(type: "integer", nullable: true),
                    LaminationMarkup = table.Column<int>(type: "integer", nullable: true),
                    CreasingPrice = table.Column<int>(type: "integer", nullable: true),
                    DrillingPrice = table.Column<int>(type: "integer", nullable: true),
                    RoundingPrice = table.Column<int>(type: "integer", nullable: true),
                    Price = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historys_ConsumablePrices_ConsumablePriceId",
                        column: x => x.ConsumablePriceId,
                        principalTable: "ConsumablePrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historys_HistoryInputs_InputId",
                        column: x => x.InputId,
                        principalTable: "HistoryInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historys_LaminationPrices_LaminationPricesId",
                        column: x => x.LaminationPricesId,
                        principalTable: "LaminationPrices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Historys_PricePapers_PricePaperId",
                        column: x => x.PricePaperId,
                        principalTable: "PricePapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryInputs_LaminationId",
                table: "HistoryInputs",
                column: "LaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryInputs_PaperId",
                table: "HistoryInputs",
                column: "PaperId");

            migrationBuilder.CreateIndex(
                name: "IX_Historys_ConsumablePriceId",
                table: "Historys",
                column: "ConsumablePriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Historys_InputId",
                table: "Historys",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_Historys_LaminationPricesId",
                table: "Historys",
                column: "LaminationPricesId");

            migrationBuilder.CreateIndex(
                name: "IX_Historys_PricePaperId",
                table: "Historys",
                column: "PricePaperId");

            migrationBuilder.CreateIndex(
                name: "IX_LaminationPrices_LaminationId",
                table: "LaminationPrices",
                column: "LaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_PaperCatalogs_SizeId",
                table: "PaperCatalogs",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_PricePapers_CatalogId",
                table: "PricePapers",
                column: "CatalogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historys");

            migrationBuilder.DropTable(
                name: "ConsumablePrices");

            migrationBuilder.DropTable(
                name: "HistoryInputs");

            migrationBuilder.DropTable(
                name: "LaminationPrices");

            migrationBuilder.DropTable(
                name: "PricePapers");

            migrationBuilder.DropTable(
                name: "Laminations");

            migrationBuilder.DropTable(
                name: "PaperCatalogs");

            migrationBuilder.DropTable(
                name: "SizePapers");
        }
    }
}
