using Common.Model;
using Logic.API;
using System;

namespace ViewModels.DiscountViewModels
{
    // view model for creating a new publish date discount
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
