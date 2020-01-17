using Common.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.API;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ViewModels.DiscountViewModels
{
    // a base view model for creating discounts
    public abstract class DiscountFormBaseViewModel : ValidationViewModelBase
    {
        IDiscountService discountService;
        public DiscountFormBaseViewModel(IDiscountService discountService)
        {
            this.discountService = discountService;
            AddDiscountCommand = new RelayCommand(AddDiscount);
        }

        [Required(AllowEmptyStrings = false)]
        [Range(1, 99,ErrorMessage="Value must be between 1 and 99")]
        public string DiscountAmount { get; set; }

        public string ErrorMsg { get; set; }

        //command to add the discount
        public RelayCommand AddDiscountCommand { get; set; }

        public abstract void AddDiscount();

        // Helper method for derived classes to use to add the discount
        protected async void AddDiscount(BaseDiscount discount)
        {
            try
            {
                var newDiscount = await discountService.AddDiscountAsync(discount);

                //sends a message to notify a new discount was created
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

        //cleans the view model after creating a discount
        protected virtual void CleanForm()
        {
            DiscountAmount = "";
            RaisePropertyChanged("DiscountAmount");
        }
    }
}
