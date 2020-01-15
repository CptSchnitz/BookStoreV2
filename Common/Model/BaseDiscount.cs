using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Model
{
    public abstract class BaseDiscount
    {
        public int Id { get; set; }
        public BaseDiscount(int amount)
        {
            DiscountAmount = amount;
        }
        public int DiscountAmount { get; set; }
        public abstract bool IsDiscountValid(AbstractItem item);

    }
}
