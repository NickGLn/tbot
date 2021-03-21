using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotTest.Model;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using TelegramBotTest.ReplyMarkups;
using Telegram.Bot.Args;

namespace TelegramBotTest.Commands
{
    public class IncomeSumCommand : Command
    {
        public override string Trigger { get; }

        public override async Task Execute(AppUser user, EventArgs args, TelegramBotClient client)
        {
            MessageEventArgs telegramArgs = (MessageEventArgs)args;
            Message message = telegramArgs.Message;

            double incomeSum;

            if (double.TryParse(message.Text, out incomeSum))
            {
                var rkm = MainMenuMarkup.Create();

                // добавляем расход и меняем стейт беседы
                if (user.ModifyExpenseId == null)
                {
                    CreateIncome(user, incomeSum);
                    ChangeUserState(user, "main");
                    UpdateModifyingTransactionCategoryId(user, null);

                    // отвечаем пользователю
                    await client.SendTextMessageAsync(message.Chat.Id, $"Добавлен доход: {incomeSum.ToString()}", replyMarkup: rkm);
                }
                else
                {
                    UpdateIncome(user, incomeSum);
                    UpdateModifyingExpenseId(user, null);
                    ChangeUserState(user, "main");

                    // отвечаем пользователю
                    await client.SendTextMessageAsync(message.Chat.Id, $"Доход успешно изменен: {incomeSum.ToString()}", replyMarkup: rkm);

                }


            }


        }

        private static void CreateIncome(AppUser user, double incomeSum)
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                Income newIncome = new Income(user.Id, (int)user.ModifyTransactionCategoryId, incomeSum);

                ctx.Incomes.Add(newIncome);
                ctx.SaveChangesAsync();
            }
        }

        private static void UpdateIncome(AppUser user, double income)
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                Income newIncome = ctx.Incomes.Find(user.ModifyExpenseId);
                ctx.Incomes.Add(newIncome);
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
