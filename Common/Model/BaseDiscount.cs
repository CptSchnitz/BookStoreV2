using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public abstract class BaseDiscount
    {
        //For ef use
        protected BaseDiscount() { }
        public int Id { get; set; }
        public BaseDiscount(int amount)
        {
            DiscountAmount = amount;
        }
        [Required]
        [Range(1,99)]
        public int DiscountAmount { get; set; }

        public string Discriminator { get; set; }

        [NotMapped]
        public decimal DiscountMulti { get => 1 - DiscountAmount / 100m; }
        // every discount needs to overide this method so you can check if discount is applicable
        public abstract bool IsDiscountValid(AbstractItem item);
        public abstract bool IsSameDiscount(BaseDiscount other);

        public object Last()
        {
            throw new NotImplementedException();
        }
    }
}
