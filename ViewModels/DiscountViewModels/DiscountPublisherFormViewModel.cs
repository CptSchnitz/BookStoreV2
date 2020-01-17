using Common.Model;
using GalaSoft.MvvmLight.Command;
using Logic.API;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace ViewModels.DiscountViewModels
{
    //view model for creating a publisher discount
    public class DiscountPublisherFormViewModel : DiscountFormBaseViewModel
    {
        IPublisherService publisherService;
        public DiscountPublisherFormViewModel(IDiscountService discountService, IPublisherService publisherService) : base(discountService)
        {
            this.publisherService = publisherService;
            LoadPublishersCommand = new RelayCommand(LoadPublishers);
        }

        //command for loading the discounts
        public RelayCommand LoadPublishersCommand { get; private set; }
        public ObservableCollection<Publisher> PublisherList { get; set; }
        public Publisher SelectedPublisher { get; set; }
        public async void LoadPublishers()
        {
            try
            {
                var list = await publisherService.GetPublishersAsync();
                PublisherList = new ObservableCollection<Publisher>(list);
                ErrorMsg = null;
                RaisePropertyChanged(nameof(PublisherList));
            }
            catch (DataException e)
            {
                ErrorMsg = e.Message;
            }
            finally
            {
                RaisePropertyChanged(nameof(ErrorMsg));
            }

            // sets the selected publisher at the start
            SelectedPublisher = PublisherList.FirstOrDefault();
            RaisePropertyChanged(nameof(SelectedPublisher));
        }

        public override void AddDiscount()
        {
            AddDiscount(new PublisherDiscount(int.Parse(DiscountAmount), SelectedPublisher));
        }
    }
}
