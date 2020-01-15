using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class PublishDateDiscount : BaseDiscount
    {
        public PublishDateDiscount(DateTime publishDate,int amount) : base(amount)
        {
            Date = publishDate.Date;
        }

        public DateTime Date { get; set; }
        public override bool IsDiscountValid(AbstractItem item)
        {
            return item.PublishDate.Date == Date;
        }
    }
}
