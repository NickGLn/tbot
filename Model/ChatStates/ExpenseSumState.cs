using System;
using System.Collections.Generic;
using TelegramBotTest.Commands;

namespace TelegramBotTest.Model
{
    public class ExpenseSumState : IChatState
    {
        public List<Command> Commands => new List<Command>() { new ExpenseSumCommand() };
    }
}
