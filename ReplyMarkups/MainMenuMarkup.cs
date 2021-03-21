using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotTest.ReplyMarkups
{
    public static class MainMenuMarkup
    {
        public static ReplyKeyboardMarkup Create()
        {
            var rkm = new ReplyKeyboardMarkup();

            rkm.Keyboard =
                new KeyboardButton[][]
                    {
                    new KeyboardButton[]
                        {
                            new KeyboardButton("Добавить расход"),
                            new KeyboardButton("Добавить доход")
                        },
                        new KeyboardButton[]
                        {
                            new KeyboardButton("Посмотреть статистику (пока не работает)"),
                            new KeyboardButton("Изменить расход")
                        }
                    };

            return rkm;
        }

    }
}
