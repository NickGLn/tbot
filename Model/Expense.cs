using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TelegramBotTest.Model

{
    public class Expense : Transaction
    {
        public Expense(int id, long userId, int categoryId, double sum, DateTime created) : base(id, userId, categoryId, sum, created) { }
        public Expense(long userId, int categoryId, double sum) : base(userId, categoryId, sum) { }

        // Я добавил перегрузку конструктора, т.к. мне надо доставать данные по затратам из базы
        // , а для этого мне нужен новый объект ,в который я запишу сразу все его свойства
        public Expense() : base() { }
    }

    
}
