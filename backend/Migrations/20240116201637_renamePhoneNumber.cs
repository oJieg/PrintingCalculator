using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace printing_calculator.Migrations
{
    public partial class renamePhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNmbers_Contacts_ContactId",
                table: "PhoneNmbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNmbers",
                table: "PhoneNmbers");

            migrationBuilder.RenameTable(
                name: "PhoneNmbers",
                newName: "PhoneNumbers");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNmbers_ContactId",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_ContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Contacts_ContactId",
                table: "PhoneNumbers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Contacts_ContactId",
                table: "PhoneNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers");

            migrationBuilder.RenameTable(
                name: "PhoneNumbers",
                newName: "PhoneNmbers");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_ContactId",
                table: "PhoneNmbers",
                newName: "IX_PhoneNmbers_ContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNmbers",
                table: "PhoneNmbers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNmbers_Contacts_ContactId",
                table: "PhoneNmbers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
