using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotTest.Model;

namespace TelegramBotTest.Commands
{
    public abstract class Command
    {
        /// <summary>
        /// здесь хранится строка, на которую треггерится команда
        /// </summary>
        public abstract string Trigger {get; }

        /// <summary>
        /// В методе должна содержаться некоторая логика команды.
        /// </summary>
        /// <param name="user">Ссылка на экземпляр пользователя</param>
        /// <param name="args">Аргументы, переданные ботом вместе с сообщением</param>
        /// <param name="client">Экземпляр клиента бота</param>
        /// <returns></returns>
        public abstract Task Execute(AppUser user, EventArgs args, TelegramBotClient client);

        // Метод проверяет, соответствует ли сообщение пользователя строке, на которую триггериться команда
        // По умолчанию, он сравнивает сообщение и триггер комманды, но для некоторых комманд может быть переопределен
        public virtual bool Match(string message) => message == Trigger;
    }
}
