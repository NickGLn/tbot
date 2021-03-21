using System;
using System.Collections.Generic;
using TelegramBotTest.Commands;

namespace TelegramBotTest.Model
{
    public class IncomeSumState : IChatState
    {
        public List<Command> Commands => new List<Command>() { new IncomeSumCommand() };
    }
}
