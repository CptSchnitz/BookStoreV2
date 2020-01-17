using System;

namespace Common.Model
{
    public class PublishDateDiscount : BaseDiscount
    {
        //for ef use
        protected PublishDateDiscount() { }

        public PublishDateDiscount(DateTime publishDate, int amount) : base(amount)
        {
            // this way you save only the date part, and cut the time
            Date = publishDate.Date;
        }

        public DateTime Date { get; set; }
        public override bool IsDiscountValid(AbstractItem item)
        {
            return item.PublishDate.Date == Date;
        }

        public override bool IsSameDiscount(BaseDiscount other)
        {
            var otherDateDiscount = other as PublishDateDiscount;
            if (otherDateDiscount == null) return false;
            return otherDateDiscount.Date == Date;
        }
    }
}
