using Microsoft.EntityFrameworkCore.Migrations;

namespace TelegramBotTest.Migrations
{
    public partial class Added_Expenses_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AppUserAccount_AccountId",
                table: "AppUsers");

            migrationBuilder.DropTable(
                name: "AppUserAccount");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "AppUsers",
                newName: "ExpensesId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_AccountId",
                table: "AppUsers",
                newName: "IX_AppUsers_ExpensesId");

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    Sum = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Expenses_ExpensesId",
                table: "AppUsers",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Expenses_ExpensesId",
                table: "AppUsers");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.RenameColumn(
                name: "ExpensesId",
                table: "AppUsers",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_ExpensesId",
                table: "AppUsers",
                newName: "IX_AppUsers_AccountId");

            migrationBuilder.CreateTable(
                name: "AppUserAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Balance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserAccount", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AppUserAccount_AccountId",
                table: "AppUsers",
                column: "AccountId",
                principalTable: "AppUserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
