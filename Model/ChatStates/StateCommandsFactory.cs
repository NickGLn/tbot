using System;
using System.Collections.Generic;
using System.Text;
using TelegramBotTest.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotTest.Model
{
    public static class StateCommandsFactory
    {
        public static IChatState Create(AppUser user)
        {
            switch(user.ChatState)
            {
                case "main":
                    return new MainState();
                case "add expense":
                    return new ExpenseSumState();
                case "add income":
                    return new IncomeSumState();
                case "modify expense id":
                    return new ModifyExpenseIdState();
                case "transaction category":
                    return new TransactionCategoryState();
                default:
                    return new MainState();
            }
        }
    }



}
