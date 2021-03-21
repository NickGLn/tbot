using System;
using System.Collections.Generic;
using System.Text;
using TelegramBotTest.Commands;

namespace TelegramBotTest.Model
{
    public class TransactionCategoryState : IChatState
    {
        public List<Command> Commands => new List<Command>() { new TransactionCategoryCommand() };
    }
}
