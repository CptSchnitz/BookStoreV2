using System;
using System.Collections.Generic;
using System.Text;
using Logic.API;

namespace ViewModels
{
    public class DiscountPublisherFormViewModel : DiscountFormBaseViewModel
    {
        IPublisherService publisherService;
        public DiscountPublisherFormViewModel(IDiscountService discountService, IPublisherService publisherService) : base(discountService)
        {
            this.publisherService = publisherService;
        }
    }
}
