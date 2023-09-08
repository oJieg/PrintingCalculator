using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace printing_calculator.Migrations
{
    public partial class AddCrm3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Contacts_ContactsId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Orders_OrdersId",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "ContactOrder");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_OrdersId",
                table: "ContactOrder",
                newName: "IX_ContactOrder_OrdersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactOrder",
                table: "ContactOrder",
                columns: new[] { "ContactsId", "OrdersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ContactOrder_Contacts_ContactsId",
                table: "ContactOrder",
                column: "ContactsId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactOrder_Orders_OrdersId",
                table: "ContactOrder",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactOrder_Contacts_ContactsId",
                table: "ContactOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactOrder_Orders_OrdersId",
                table: "ContactOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactOrder",
                table: "ContactOrder");

            migrationBuilder.RenameTable(
                name: "ContactOrder",
                newName: "Enrollments");

            migrationBuilder.RenameIndex(
                name: "IX_ContactOrder_OrdersId",
                table: "Enrollments",
                newName: "IX_Enrollments_OrdersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                columns: new[] { "ContactsId", "OrdersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Contacts_ContactsId",
                table: "Enrollments",
                column: "ContactsId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Orders_OrdersId",
                table: "Enrollments",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
