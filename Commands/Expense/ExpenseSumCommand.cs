using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotTest.Model;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotTest.ReplyMarkups;
using Telegram.Bot.Args;

namespace TelegramBotTest.Commands
{
    public class ExpenseSumCommand : Command
    {
        public int ExpenseId { get; }
        public ExpenseSumCommand() { }
        public ExpenseSumCommand(int expenseId)
        {
            ExpenseId = expenseId;
        }
        public override string Trigger { get; }

        public override async Task Execute(AppUser user, EventArgs args, TelegramBotClient client)
        {
            MessageEventArgs telegramArgs = (MessageEventArgs)args;
            Message message = telegramArgs.Message;

            double expenseSum;

            if (double.TryParse(message.Text, out expenseSum))
            {
                var rkm = MainMenuMarkup.Create();

                // добавляем расход и меняем стейт беседы
                if (user.ModifyExpenseId == null)
                {
                    CreateExpense(user, expenseSum);
                    ChangeUserState(user, "main");
                    UpdateModifyingTransactionCategoryId(user, null);

                    // отвечаем пользователю
                    await client.SendTextMessageAsync(message.Chat.Id, $"Добавлен расход: {expenseSum.ToString()}", replyMarkup: rkm);
                }
                else
                {
                    UpdateExpense(user, expenseSum);
                    UpdateModifyingExpenseId(user, null);
                    ChangeUserState(user, "main");

                    // отвечаем пользователю
                    await client.SendTextMessageAsync(message.Chat.Id, $"Расход успешно изменен: {expenseSum.ToString()}", replyMarkup: rkm);

                }


            }
        }

        private static void CreateExpense(AppUser user, double expenseSum)
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                Expense newExpense = new Expense(user.Id, (int)user.ModifyTransactionCategoryId, expenseSum);

                ctx.Expenses.Add(newExpense);
                ctx.SaveChangesAsync();
            }
        }

        private static void UpdateExpense(AppUser user, double expenseSum)
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                Expense newExpense = ctx.Expenses.Find(user.ModifyExpenseId);
                newExpense.Sum = expenseSum;
                ctx.Expenses.Update(newExpense);
                ctx.SaveChangesAsync();
            }
        }

        private static void ChangeUserState(AppUser user, string nextState)
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                user.ChatState = nextState;
                ctx.AppUsers.Update(user);
                ctx.SaveChangesAsync();
            }
        }

        private static void UpdateModifyingExpenseId(AppUser user, int? expenseId)
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                user.ModifyExpenseId = expenseId;
                ctx.AppUsers.Update(user);
                ctx.SaveChangesAsync();
            }
        }

        private static void UpdateModifyingTransactionCategoryId(AppUser user, int? transactionId)
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                user.ModifyTransactionCategoryId = transactionId;
                ctx.AppUsers.Update(user);
                ctx.SaveChangesAsync();
            }
        }

        public override bool Match(string message)
        {
            Match m = Regex.Match(message, @"^\d+\.?\d*$", RegexOptions.IgnoreCase);
            return m.Success;
        }
    }
}
