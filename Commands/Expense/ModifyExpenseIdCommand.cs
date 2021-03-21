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
    public class ModifyExpenseIdCommand : Command
    {
        public override string Trigger { get; }

        public override async Task Execute(AppUser user, EventArgs args, TelegramBotClient client)
        {
            CallbackQueryEventArgs telegramArgs = (CallbackQueryEventArgs)args;
            Message message = telegramArgs.CallbackQuery.Message;
            string callbackData = telegramArgs.CallbackQuery.Data;

            int expenseId;

            if (int.TryParse(callbackData, out expenseId))
            {
                ChangeUserState(user, "transaction category");
                ChangeUserModifyingExpense(user, expenseId);

                await client.SendTextMessageAsync(message.Chat.Id, $"Выберите категорию:");
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

        private static void ChangeUserModifyingExpense(AppUser user, int expenseId)
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                user.ModifyExpenseId = expenseId;
                ctx.AppUsers.Update(user);
                ctx.SaveChangesAsync();
            }
        }

        public override bool Match(string message)
        {
            Match m = Regex.Match(message, @"^\d+", RegexOptions.IgnoreCase);
            return m.Success;
        }
    }
}
