using Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using ViewModels.Services;

namespace ViewModels
{
    public class DiscountListViewModel : ViewModelBase
    {
        IDiscountService discountService;
        IMessageBoxService messageService;

        public DiscountListViewModel(IDiscountService discountService, IMessageBoxService messageService)
        {
            this.discountService = discountService;
            this.messageService = messageService;
            LoadDiscountsCommand = new RelayCommand(LoadDiscounts);
            RemoveDiscountCommand = new RelayCommand<BaseDiscount>(RemoveDiscount);
            Messenger.Default.Register<BaseDiscount>(this, (discount) => LoadDiscounts());
        }

        public RelayCommand LoadDiscountsCommand { get; private set; }
        public RelayCommand<BaseDiscount> RemoveDiscountCommand { get; private set; }
        public ObservableCollection<BaseDiscount> DiscountList { get; set; }

        public string ErrorMsg { get; set; }

        public async void LoadDiscounts()
        {
            try
            {
                var list = await discountService.GetDiscountsAsync();
                DiscountList = new ObservableCollection<BaseDiscount>(list);
                ErrorMsg = null;
                RaisePropertyChanged(nameof(DiscountList));
            }
            catch (Exception e)
            {
                ErrorMsg = e.Message;
            }
            finally
            {
                RaisePropertyChanged(nameof(ErrorMsg));
            }
        }

        public async void RemoveDiscount(BaseDiscount discount)
        {
            if (discount != null && messageService.ShowOkCancelMessage("Delete discount", "Are you sure?"))
            {
                try
                {
                    await discountService.RemoveDiscountAsync(discount);
                    ErrorMsg = null;
                    RaisePropertyChanged(nameof(DiscountList));
                    DiscountList.Remove(discount);
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
        }
    }
}

