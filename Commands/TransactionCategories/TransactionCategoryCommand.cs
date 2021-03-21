using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotTest.Model;

namespace TelegramBotTest.Commands
{
    public class TransactionCategoryCommand : Command
    {
        public override string Trigger { get; }

        public override async Task Execute(AppUser user, EventArgs args, TelegramBotClient client)
        {
            int transactionCategoryId;
            CallbackQueryEventArgs telegramArgs = (CallbackQueryEventArgs)args;
            Message message = telegramArgs.CallbackQuery.Message;
            string callbackData = telegramArgs.CallbackQuery.Data;

            using AppDbContext ctx = new AppDbContext();
            if (int.TryParse(callbackData, out transactionCategoryId))
            {
                var transaction = ctx.TransactionCategories
                    .Where(x => x.Id == transactionCategoryId)
                    .First();

                if (transaction.TransactionType == 0)
                {
                    ChangeUserState(user, "add expense");
                    SetUserTransactionCategoryId(user, transactionCategoryId);
                }
                else
                {
                    ChangeUserState(user, "add income");
                    SetUserTransactionCategoryId(user, transactionCategoryId);
                }
                    await client.SendTextMessageAsync(message.Chat.Id, $"Введите сумму:");
            }
            else
            {
                await client.SendTextMessageAsync(message.Chat.Id, $"Указана несуществующая категория");
            }
            
        }

        public override bool Match(string message)
        {
            int categoryId;
            using AppDbContext ctx = new AppDbContext();
            var dbCategories = ctx.TransactionCategories;

            if (int.TryParse(message, out categoryId))
            {
                foreach (var category in dbCategories)
                {
                    if (category.Id == categoryId) { return true; }
                }

                return false;
            }
            else
            {
                return false;
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

        private static void SetUserTransactionCategoryId(AppUser user, int transactionCategoryId)
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                user.ModifyTransactionCategoryId = transactionCategoryId;
                ctx.AppUsers.Update(user);
                ctx.SaveChangesAsync();
            }
        }
    }
}
