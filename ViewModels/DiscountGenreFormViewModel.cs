using System;
using System.Collections.Generic;
using System.Text;
using Logic.API;

namespace ViewModels
{
    public class DiscountGenreFormViewModel : DiscountFormBaseViewModel
    {
        IGenreService genreService;
        public DiscountGenreFormViewModel(IDiscountService discountService, IGenreService genreService) : base(discountService)
        {
            this.genreService = genreService;
        }
    }
}
