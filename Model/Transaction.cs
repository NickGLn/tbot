using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotTest.Model
{
    public abstract class Transaction
    {
        public Transaction(int id, long userId, int? categoryId, double sum, DateTime created)
        {
            this.Id = id;
            this.UserId = userId;
            this.Sum = sum;
            this.Created = created;
            this.CategoryId = categoryId;
        }

        public Transaction(long userId, int? categoryId, double sum)
        {
            this.UserId = userId;
            this.Sum = sum;
            this.Created = DateTime.Now;
            this.CategoryId = categoryId;
        }

        public Transaction()
        { }

        public int Id { get; set; }
        public long UserId { get; set; }
        public DateTime Created { get; set; }
        public int? CategoryId { get; set; }
        public double Sum { get; set; }
    }
}
