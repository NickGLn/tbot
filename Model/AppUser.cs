using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotTest.Model
{
    public class AppUser
    {
        public AppUser() { }
        public AppUser(long Id)
        {
            this.Id = Id;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ChatState { get; set; } = "main";
        public int? ModifyTransactionCategoryId { get; set; } = null;
        public int? ModifyExpenseId { get; set; } = null;
        public int? ModifyIncomeId { get; set; } = null;

    }
}
