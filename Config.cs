using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotTest
{
    public static class Config
    {
        public static string Token = Environment.GetEnvironmentVariable("TelegramBotApiKey").ToString();
        public static string DbPath = Environment.GetEnvironmentVariable("TelegramBotSqliteDb").ToString();
    }
}