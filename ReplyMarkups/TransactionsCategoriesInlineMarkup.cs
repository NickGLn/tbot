using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotTest.ReplyMarkups
{
    public class TransactionsCategoriesInlineMarkup
    {
        public InlineKeyboardMarkup Create()
        {
            using AppDbContext ctx = new AppDbContext();
            int count = ctx.TransactionCategories.Count();
            int index = 0;

            InlineKeyboardButton[][] keyboardInline = new InlineKeyboardButton[count][];

            foreach (var transaction in ctx.TransactionCategories.Take(count))
            {
                InlineKeyboardButton button = new InlineKeyboardButton()
                {
                    Text = $"Category: {transaction.CategoryName}",
                    CallbackData = transaction.Id.ToString()
                };

                keyboardInline[index] = new InlineKeyboardButton[] { button };
                index += 1;
            }

            InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(keyboardInline);
            return inlineKeyboardMarkup;
        }
        
    }
}
