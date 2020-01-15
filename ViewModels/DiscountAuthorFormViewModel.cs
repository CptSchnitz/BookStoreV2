using System;
using System.Collections.Generic;
using System.Text;
using Logic.API;

namespace ViewModels
{
    public class DiscountAuthorFormViewModel : DiscountFormBaseViewModel
    {
        IAuthorService authorService;
        public DiscountAuthorFormViewModel(IDiscountService discountService, IAuthorService authorService) : base(discountService)
        {
            this.authorService = authorService; 
        }
    }
}
