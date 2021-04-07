using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordWallet_console.Migrations
{
    public partial class Initial16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FunctionType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Function_name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    functionRunId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FunctionType_functionRun_functionRunId",
                        column: x => x.functionRunId,
                        principalTable: "functionRun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FunctionType_functionRunId",
                table: "FunctionType",
                column: "functionRunId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FunctionType");
        }
    }
}
