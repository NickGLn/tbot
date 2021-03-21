using Microsoft.EntityFrameworkCore.Migrations;

namespace TelegramBotTest.Migrations
{
    public partial class Added_Id_to_TransactionCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "modifyIncomeId",
                table: "AppUsers",
                newName: "ModifyIncomeId");

            migrationBuilder.RenameColumn(
                name: "modifyExpenseId",
                table: "AppUsers",
                newName: "ModifyExpenseId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Incomes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Expenses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifyTransactionCategoryId",
                table: "AppUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransactionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatorId = table.Column<long>(type: "INTEGER", nullable: false),
                    TransactionType = table.Column<int>(type: "INTEGER", nullable: false),
                    TransactionTypeName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionCategories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "ModifyTransactionCategoryId",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "ModifyIncomeId",
                table: "AppUsers",
                newName: "modifyIncomeId");

            migrationBuilder.RenameColumn(
                name: "ModifyExpenseId",
                table: "AppUsers",
                newName: "modifyExpenseId");
        }
    }
}
