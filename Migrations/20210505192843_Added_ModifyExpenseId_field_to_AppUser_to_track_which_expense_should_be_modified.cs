using Microsoft.EntityFrameworkCore.Migrations;

namespace TelegramBotTest.Migrations
{
    public partial class Added_ModifyExpenseId_field_to_AppUser_to_track_which_expense_should_be_modified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "modifyExpenseId",
                table: "AppUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "modifyIncomeId",
                table: "AppUsers",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "modifyExpenseId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "modifyIncomeId",
                table: "AppUsers");
        }
    }
}
