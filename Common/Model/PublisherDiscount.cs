using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public class PublisherDiscount : BaseDiscount
    {
        //for ef use
        protected PublisherDiscount() { }
        public PublisherDiscount(Publisher publisher, int amount) : base(amount)
        {
            if (publisher is null)
            {
                throw new System.ArgumentNullException(nameof(publisher));
            }

            PublisherId = publisher.Id;
        }

        [ForeignKey(nameof(Publisher))]
        [Required]
        public int PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }
        public override bool IsDiscountValid(AbstractItem item)
        {
            return item.PublisherId == PublisherId;
        }

        public override bool IsSameDiscount(BaseDiscount other)
        {
            var otherPublisherDiscount = other as PublisherDiscount;
            if (otherPublisherDiscount == null) return false;
            return otherPublisherDiscount.PublisherId == PublisherId;
        }
    }
}
