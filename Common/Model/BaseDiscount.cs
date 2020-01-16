using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
        public int DiscountAmount { get; set; }

        public string Discriminator { get; set; }

        [NotMapped]
        public decimal DiscountMulti { get => 1 - DiscountAmount / 100m; }
        public abstract bool IsDiscountValid(AbstractItem item);
        public abstract bool IsSameDiscount(BaseDiscount other);
    }
}
