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
                    Name = table.Column<string>(type: "text", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false)
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
                name: "InputsHistories",
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
                    table.PrimaryKey("PK_InputsHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InputsHistories_Laminations_LaminationId",
                        column: x => x.LaminationId,
                        principalTable: "Laminations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InputsHistories_PaperCatalogs_PaperId",
                        column: x => x.PaperId,
                        principalTable: "PaperCatalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaperPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<float>(type: "real", nullable: false),
                    CatalogId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaperPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaperPrices_PaperCatalogs_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "PaperCatalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InputId = table.Column<int>(type: "integer", nullable: false),
                    PaperPriceId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_ConsumablePrices_ConsumablePriceId",
                        column: x => x.ConsumablePriceId,
                        principalTable: "ConsumablePrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Histories_InputsHistories_InputId",
                        column: x => x.InputId,
                        principalTable: "InputsHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Histories_LaminationPrices_LaminationPricesId",
                        column: x => x.LaminationPricesId,
                        principalTable: "LaminationPrices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Histories_PaperPrices_PaperPriceId",
                        column: x => x.PaperPriceId,
                        principalTable: "PaperPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Histories_ConsumablePriceId",
                table: "Histories",
                column: "ConsumablePriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_InputId",
                table: "Histories",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_LaminationPricesId",
                table: "Histories",
                column: "LaminationPricesId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_PaperPriceId",
                table: "Histories",
                column: "PaperPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_InputsHistories_LaminationId",
                table: "InputsHistories",
                column: "LaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_InputsHistories_PaperId",
                table: "InputsHistories",
                column: "PaperId");

            migrationBuilder.CreateIndex(
                name: "IX_LaminationPrices_LaminationId",
                table: "LaminationPrices",
                column: "LaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_PaperCatalogs_SizeId",
                table: "PaperCatalogs",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaperPrices_CatalogId",
                table: "PaperPrices",
                column: "CatalogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
