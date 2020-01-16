using Common.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace ViewModels.DiscountViewModels
{
    public abstract class DiscountFormBaseViewModel : ValidationViewModelBase
    {
        IDiscountService discountService;
        public DiscountFormBaseViewModel(IDiscountService discountService)
        {
            this.discountService = discountService;
            AddDiscountCommand = new RelayCommand(AddDiscount);
        }

        [Required(AllowEmptyStrings = false)]
        [Range(1,99)]
        public string DiscountAmount { get; set; }

        public string ErrorMsg { get; set; }

        public RelayCommand AddDiscountCommand { get; set; }

        public abstract void AddDiscount();
        protected async void AddDiscount(BaseDiscount discount)
        {
            try
            {
                var newDiscount = await discountService.AddDiscountAsync(discount);
                Messenger.Default.Send<BaseDiscount>(newDiscount);
                ErrorMsg = null;
                CleanForm();
            }
            catch (DataException e)
            {
                ErrorMsg = e.Message;
            }
            finally
            {
                RaisePropertyChanged(nameof(ErrorMsg));
            }
        }

        protected virtual void CleanForm()
        {
            DiscountAmount = "";
            RaisePropertyChanged("DiscountAmount");
        }
    }
}
