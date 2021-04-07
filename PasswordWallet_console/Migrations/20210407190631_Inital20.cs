using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordWallet_console.Migrations
{
    public partial class Inital20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FunctionType");

            migrationBuilder.CreateTable(
                name: "ActionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataChanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreviousValue = table.Column<string>(nullable: true),
                    PresentValue = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Userid = table.Column<int>(nullable: false),
                    ActionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataChanges", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionTypes");

            migrationBuilder.DropTable(
                name: "DataChanges");

            migrationBuilder.CreateTable(
                name: "FunctionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    functionRunId = table.Column<int>(type: "int", nullable: true),
                    Function_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
