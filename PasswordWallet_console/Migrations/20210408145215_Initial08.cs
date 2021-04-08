using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordWallet_console.Migrations
{
    public partial class Initial08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DataChanges_ActionTypeId",
                table: "DataChanges",
                column: "ActionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataChanges_ActionTypes_ActionTypeId",
                table: "DataChanges",
                column: "ActionTypeId",
                principalTable: "ActionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataChanges_ActionTypes_ActionTypeId",
                table: "DataChanges");

            migrationBuilder.DropIndex(
                name: "IX_DataChanges_ActionTypeId",
                table: "DataChanges");
        }
    }
}
