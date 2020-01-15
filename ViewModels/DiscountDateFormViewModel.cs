using System;
using System.Collections.Generic;
using System.Text;
using Logic.API;

namespace ViewModels
{
    public class DiscountDateFormViewModel : DiscountFormBaseViewModel
    {
        public DiscountDateFormViewModel(IDiscountService discountService) : base(discountService)
        {
        }
    }
}
