using System;
using System.Collections.Generic;
using System.Text;
using Common.Model;
using Logic.API;

namespace ViewModels
{
    public class DiscountDateFormViewModel : DiscountFormBaseViewModel
    {
        public DiscountDateFormViewModel(IDiscountService discountService) : base(discountService)
        {
        }

        public DateTime DiscountDate { get; set; } = DateTime.Now;
       
        public override void AddDiscount()
        {
            AddDiscount(new PublishDateDiscount(DiscountDate, int.Parse(DiscountAmount)));
        }
    }
}
