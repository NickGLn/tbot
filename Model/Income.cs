using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotTest.Model
{
    public class Income : Transaction
    {
        public Income(long userId, int? categoryId, double sum) : base(userId, categoryId, sum) { }
        public Income() : base() { }
    }
}
