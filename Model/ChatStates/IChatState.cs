using System;
using System.Collections.Generic;
using System.Text;
using TelegramBotTest.Commands;

namespace TelegramBotTest.Model
{
    public interface IChatState
    {
        List<Command> Commands { get; }
    }
}
