using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordWallet_console.Migrations
{
    public partial class Inital21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModifiedRecord",
                table: "DataChanges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DataChanges_Userid",
                table: "DataChanges",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_DataChanges_Users_Userid",
                table: "DataChanges",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataChanges_Users_Userid",
                table: "DataChanges");

            migrationBuilder.DropIndex(
                name: "IX_DataChanges_Userid",
                table: "DataChanges");

            migrationBuilder.DropColumn(
                name: "ModifiedRecord",
                table: "DataChanges");
        }
    }
}
