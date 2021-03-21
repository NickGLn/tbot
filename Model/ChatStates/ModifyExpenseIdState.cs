using System;
using System.Collections.Generic;
using TelegramBotTest.Commands;

namespace TelegramBotTest.Model
{
    public class ModifyExpenseIdState : IChatState
    {
        public List<Command> Commands => new List<Command>() { new ModifyExpenseIdCommand() };
    }
}
