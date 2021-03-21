using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotTest.Model;

namespace TelegramBotTest.Commands
{
    public class CreateExpenseCommand: Command
    {
        public override string Trigger { get; } = "Добавить расход";

        public override async Task Execute(AppUser user, EventArgs args, TelegramBotClient client)
        {
            MessageEventArgs telegramArgs = (MessageEventArgs)args;
            Message message = telegramArgs.Message;
            Console.WriteLine(message.Text, Trigger);

            // Меняем стейт беседы у пользователя
            UpdateUser(user, "transaction category");

            using AppDbContext ctx = new AppDbContext();
            int count = ctx.TransactionCategories.Count();
            int index = 0;

            InlineKeyboardButton[][] keyboardInline = new InlineKeyboardButton[count][];

            foreach (var transacts in ctx.TransactionCategories.Take(count))
            {
                InlineKeyboardButton button = new InlineKeyboardButton()
                {
                    Text = $"Id: {transacts.Id}, Category: {transacts.CategoryName}",
                    CallbackData = transacts.Id.ToString()
                };

                keyboardInline[index] = new InlineKeyboardButton[] { button };
                index += 1;
            }


            InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(keyboardInline);

            // Отправляем ответное сообщение
            await client.SendTextMessageAsync(message.Chat.Id, $"Выберите категорию расходов: ", replyMarkup: inlineKeyboardMarkup);
        }

        private static void UpdateUser(AppUser user, string nextState)
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                user.ChatState = nextState;
                ctx.AppUsers.Update(user);
                ctx.SaveChangesAsync();
            }
        }
    }
}
