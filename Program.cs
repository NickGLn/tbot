using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Collections.Generic;
using TelegramBotTest.Commands;
using TelegramBotTest.Model;
using Telegram.Bot.Types;
using Microsoft.EntityFrameworkCore;

namespace TelegramBotTest
{
    class Program
        {
        private static TelegramBotClient client;
        static void Main(string[] args)
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                ctx.Database.Migrate();
            }

            client = new TelegramBotClient(Config.Token);
            client.StartReceiving();
            client.OnMessage += ChatInputHandler.OnNewMessage;
            client.OnCallbackQuery += ChatInputHandler.OnCallback;
            Console.WriteLine("[Log] start receiving!");
            Console.ReadLine();
            client.StopReceiving();

        }
    }
}
