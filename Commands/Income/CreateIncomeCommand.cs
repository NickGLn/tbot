using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotTest.Model;

namespace TelegramBotTest.Commands
{
    public class CreateIncomeCommand : Command
    {
        public override string Trigger { get; } = "Добавить доход";

        public override async Task Execute(AppUser user, EventArgs args, TelegramBotClient client)
        {
            MessageEventArgs telegramArgs = (MessageEventArgs)args;
            Message message = telegramArgs.Message;

            // Меняем стейт беседы у пользователя
            ChangeUserState(user, "transaction category");

            // Данный объект нужен для удаления текущих кнопок из чата с пользоателем
            var removeMarkup = new ReplyKeyboardRemove() { };

            // Отправляем ответное сообщение
            await client.SendTextMessageAsync(message.Chat.Id, $"Введите доход: ", replyMarkup: removeMarkup);
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
    }
}
