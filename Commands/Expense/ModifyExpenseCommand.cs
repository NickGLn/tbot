using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotTest.Model;
using Telegram.Bot.Args;

namespace TelegramBotTest.Commands
{
    public class ModifyExpenseCommand : Command
    {
        public override string Trigger { get; } = "Изменить расход";

        public string CallBackData { get; set; }

        public override async Task Execute(AppUser user, EventArgs args, TelegramBotClient client)
        {
            MessageEventArgs telegramArgs = (MessageEventArgs)args;
            Message message = telegramArgs.Message;

            // Меняем стейт беседы у пользователя
            using (AppDbContext ctx = new AppDbContext())
            {
                int count = ctx.Expenses.Count(r => r.UserId == user.Id);
                int index = 0;

                InlineKeyboardButton[][] keyboardInline = new InlineKeyboardButton[count][];

                foreach (var expense in ctx.Expenses.Take(count))
                {
                    InlineKeyboardButton button = new InlineKeyboardButton()
                    {
                        Text = $"Id: {expense.Id}, Created: {expense.Created}, Value: {expense.Sum}",
                        CallbackData = expense.Id.ToString()
                    };

                    keyboardInline[index] = new InlineKeyboardButton[] { button };
                    index += 1;
                }
                
                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(keyboardInline);
                await client.SendTextMessageAsync(message.Chat.Id, $"Нажмите на расход, который требуется изменить", replyMarkup: inlineKeyboardMarkup);

                user.ChatState = "modify expense id";
                ctx.AppUsers.Update(user);
                ctx.SaveChangesAsync();

            }
        }

        
    }
}
