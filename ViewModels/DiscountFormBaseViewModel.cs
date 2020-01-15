using Logic.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public abstract class DiscountFormBaseViewModel : ValidationViewModelBase
    {
        IDiscountService discountService;
        public DiscountFormBaseViewModel(IDiscountService discountService)
        {
            this.discountService = discountService;
        }
    }
}
