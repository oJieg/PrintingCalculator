using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace printing_calculator.Migrations
{
    public partial class start : Migration
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
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laminations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
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
                name: "CommonToAllMarkups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PercentMarkup = table.Column<int>(type: "integer", nullable: false),
                    Adjustmen = table.Column<int>(type: "integer", nullable: false),
                    SettingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonToAllMarkups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonToAllMarkups_Settings_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Settings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MachineSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameMachine = table.Column<string>(type: "text", nullable: false),
                    ConsumableOther = table.Column<float>(type: "real", nullable: false),
                    AdjustmenPrice = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    SettingId = table.Column<int>(type: "integer", nullable: true),
                    CountOfPapersInOneAdjustmentCut = table.Column<int>(type: "integer", nullable: true),
                    AddMoreHit = table.Column<float>(type: "real", nullable: true),
                    SettingId1 = table.Column<int>(type: "integer", nullable: true),
                    WhiteFieldWidth = table.Column<float>(type: "real", nullable: true),
                    WhiteFieldHeight = table.Column<float>(type: "real", nullable: true),
                    MaximumSizeLength = table.Column<int>(type: "integer", nullable: true),
                    MaximumSizeWidth = table.Column<int>(type: "integer", nullable: true),
                    FieldForLabels = table.Column<int>(type: "integer", nullable: true),
                    Bleed = table.Column<int>(type: "integer", nullable: true),
                    ConsumableDye = table.Column<int>(type: "integer", nullable: true),
                    MainConsumableForDrawing = table.Column<int>(type: "integer", nullable: true),
                    PrintingMachineSetting_SettingId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineSettings_Settings_PrintingMachineSetting_SettingId1",
                        column: x => x.PrintingMachineSetting_SettingId1,
                        principalTable: "Settings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineSettings_Settings_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Settings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MachineSettings_Settings_SettingId1",
                        column: x => x.SettingId1,
                        principalTable: "Settings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaperCatalogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SizeId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Prices = table.Column<float>(type: "real", nullable: false)
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
                name: "Markups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Page = table.Column<int>(type: "integer", nullable: false),
                    MarkupForThisPage = table.Column<int>(type: "integer", nullable: false),
                    MachineSettingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Markups_MachineSettings_MachineSettingId",
                        column: x => x.MachineSettingId,
                        principalTable: "MachineSettings",
                        principalColumn: "Id");
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
                    RoundingAmount = table.Column<bool>(type: "boolean", nullable: false),
                    CommonToAllMarkupName = table.Column<List<string>>(type: "text[]", nullable: true)
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
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InputId = table.Column<int>(type: "integer", nullable: false),
                    PaperPrice = table.Column<float>(type: "real", nullable: false),
                    ConsumablePriceId = table.Column<int>(type: "integer", nullable: false),
                    MarkupPaper = table.Column<int>(type: "integer", nullable: true),
                    CutPrice = table.Column<int>(type: "integer", nullable: true),
                    LaminationPrices = table.Column<float>(type: "real", nullable: true),
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommonToAllMarkups_Name",
                table: "CommonToAllMarkups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommonToAllMarkups_SettingId",
                table: "CommonToAllMarkups",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_ConsumablePriceId",
                table: "Histories",
                column: "ConsumablePriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_InputId",
                table: "Histories",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_InputsHistories_LaminationId",
                table: "InputsHistories",
                column: "LaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_InputsHistories_PaperId",
                table: "InputsHistories",
                column: "PaperId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSettings_NameMachine",
                table: "MachineSettings",
                column: "NameMachine",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MachineSettings_PrintingMachineSetting_SettingId1",
                table: "MachineSettings",
                column: "PrintingMachineSetting_SettingId1");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSettings_SettingId",
                table: "MachineSettings",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSettings_SettingId1",
                table: "MachineSettings",
                column: "SettingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Markups_MachineSettingId",
                table: "Markups",
                column: "MachineSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_PaperCatalogs_SizeId",
                table: "PaperCatalogs",
                column: "SizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommonToAllMarkups");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "Markups");

            migrationBuilder.DropTable(
                name: "ConsumablePrices");

            migrationBuilder.DropTable(
                name: "InputsHistories");

            migrationBuilder.DropTable(
                name: "MachineSettings");

            migrationBuilder.DropTable(
                name: "Laminations");

            migrationBuilder.DropTable(
                name: "PaperCatalogs");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "SizePapers");
        }
    }
}
