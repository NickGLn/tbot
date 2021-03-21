using Microsoft.EntityFrameworkCore.Migrations;

namespace TelegramBotTest.Migrations
{
    public partial class Rename_AppUserState_to_AppUserChatSTATE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "AppUsers",
                newName: "ChatState");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChatState",
                table: "AppUsers",
                newName: "State");
        }
    }
}
