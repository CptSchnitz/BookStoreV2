using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Model
{
    public class PublisherDiscount : BaseDiscount
    {
        public PublisherDiscount(int amount, Publisher publisher) : base(amount)
        {
            PublisherId = publisher.Id;
        }

        [ForeignKey(nameof(Publisher))]
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public override bool IsDiscountValid(AbstractItem item)
        {
            return item.PublisherId == PublisherId;
        }
    }
}
