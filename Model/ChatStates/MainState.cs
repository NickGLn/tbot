using System;
using System.Collections.Generic;
using TelegramBotTest.Commands;

namespace TelegramBotTest.Model
{
    public class MainState : IChatState
    {
        public List<Command> Commands => new List<Command>() 
        {   
            new StartCommand(), 
            new CreateExpenseCommand(),
            new CreateIncomeCommand(),
            new ModifyExpenseCommand() 
        };
    }
}
