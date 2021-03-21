using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotTest.Model
{
    public class TransactionCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public long CreatorId { get; set; }
        public int TransactionType { get; set; }
        public string TransactionTypeName { get; set; }

    }
}
