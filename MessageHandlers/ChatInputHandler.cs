using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegramBotTest.Commands;
using TelegramBotTest.Model;


namespace TelegramBotTest
{
    public static class ChatInputHandler
    {

        public async static void OnCallback(object sender, CallbackQueryEventArgs args)
        {
            TelegramBotClient client = (TelegramBotClient)sender;
            Message message = args.CallbackQuery.Message;

            using AppDbContext ctx = new AppDbContext();

            AppUser user = ctx.AppUsers.Find(args.CallbackQuery.Message.Chat.Id);
            if (user is null)
            {
                user = new AppUser()
                {
                    Id = message.Chat.Id,
                    Name = message.Chat.FirstName,
                    LastName = message.Chat.LastName,
                };
                ctx.AppUsers.Add(user);
                ctx.SaveChangesAsync();
            }
            
            IChatState state = StateCommandsFactory.Create(user);
            List<Command> commands = state.Commands;

            if (message.Text != null)
            {
                Console.WriteLine($"[Log]: New callback query from {message.From.FirstName}, phone {message.From.Id}, user data :{args.CallbackQuery.Data}");

                // Проверяем, соответствует ли сообщение, введенное юзером, какой-либо команде
                foreach (var c in commands)
                {

                    if (c.Match(args.CallbackQuery.Data))
                    {
                        await c.Execute(user, args, client);
                        break;
                    }
                }
            }
        }

        public static async void OnNewMessage(object sender, MessageEventArgs args)
        {
            TelegramBotClient client = (TelegramBotClient)sender;

            Message message = args.Message;

            AppUser user = new AppUser();

            using (AppDbContext ctx = new AppDbContext())
            {
                user = ctx.AppUsers.Find(args.Message.Chat.Id);

                if (user is null)
                {
                    user = new AppUser()
                    {
                        Id = args.Message.Chat.Id,
                        Name = args.Message.Chat.FirstName,
                        LastName = args.Message.Chat.LastName,
                    };
                    ctx.AppUsers.Add(user);
                    ctx.SaveChangesAsync();
                }
            }

            // Получаем текущий стейт юзера
            IChatState state = StateCommandsFactory.Create(user);
            List<Command> commands = state.Commands;

            if (message.Text != null)
            {
                Console.WriteLine($"[Log]: New message \"{message.Text}\" from {message.From.FirstName}, phone {message.From.Id}");

                // Проверяем, соответствует ли сообщение, введенное юзером, какой-либо команде
                foreach (var c in commands)
                {

                    if (c.Match(message.Text))
                    {
                        await c.Execute(user, args, client);
                        break;
                    }
                }
            }
        }
    }
}
