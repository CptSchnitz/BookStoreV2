using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class AuthorDiscount : BaseDiscount
    {
        public AuthorDiscount(Author author,int amount) : base(amount)
        {
            AuthorId = author.Id;
        }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public override bool IsDiscountValid(AbstractItem item)
        {
            var book = item as Book;
            if (book == null || book.AuthorId != AuthorId)
                return false;
            else
                return true;
        }
    }
}
