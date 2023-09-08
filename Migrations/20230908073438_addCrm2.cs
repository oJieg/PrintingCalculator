using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace printing_calculator.Migrations
{
    public partial class addCrm2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Orders_OrderId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_OrderId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Contacts");

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    ContactsId = table.Column<int>(type: "integer", nullable: false),
                    OrdersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => new { x.ContactsId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_Enrollments_Contacts_ContactsId",
                        column: x => x.ContactsId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_OrdersId",
                table: "Enrollments",
                column: "OrdersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Contacts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_OrderId",
                table: "Contacts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Orders_OrderId",
                table: "Contacts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
