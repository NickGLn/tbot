using Microsoft.EntityFrameworkCore.Migrations;

namespace TelegramBotTest.Migrations
{
    public partial class modified_Appuser_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Expenses_ExpensesId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_ExpensesId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ExpensesId",
                table: "AppUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpensesId",
                table: "AppUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_ExpensesId",
                table: "AppUsers",
                column: "ExpensesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Expenses_ExpensesId",
                table: "AppUsers",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
