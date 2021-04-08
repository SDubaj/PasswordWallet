using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordWallet_console.Migrations
{
    public partial class Initial05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FunctionsId",
                table: "functionRun",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_functionRun_FunctionsId",
                table: "functionRun",
                column: "FunctionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_functionRun_Functions_FunctionsId",
                table: "functionRun",
                column: "FunctionsId",
                principalTable: "Functions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_functionRun_Functions_FunctionsId",
                table: "functionRun");

            migrationBuilder.DropIndex(
                name: "IX_functionRun_FunctionsId",
                table: "functionRun");

            migrationBuilder.DropColumn(
                name: "FunctionsId",
                table: "functionRun");
        }
    }
}
