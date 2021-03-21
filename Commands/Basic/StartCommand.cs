using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotTest.Model;
using TelegramBotTest.ReplyMarkups;

namespace TelegramBotTest.Commands
{
    public class StartCommand : Command
    {
        public override string Trigger { get; } = "/start";

        public override async Task Execute(AppUser user, EventArgs args, TelegramBotClient client)
        {
            MessageEventArgs telegramArgs = (MessageEventArgs)args;
            Message message = telegramArgs.Message;

            var replyKeyboardMarkup = MainMenuMarkup.Create();
            string replyMessage = "Здарова, бродяга!. Здесь ты можешь посчитать свой лавандос!";
            user.ChatState = "main";

            await client.SendTextMessageAsync(message.Chat.Id, replyMessage, replyMarkup: replyKeyboardMarkup);
            
        }
    }
}
