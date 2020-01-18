using Common.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.API;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ViewModels
{
    // view model for adding publishers
    public class PublisherFormViewModel : ValidationViewModelBase
    {
        IPublisherService publisherService;
        public PublisherFormViewModel(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
            AddPublisherCommand = new RelayCommand(AddPublisher);
        }

        #region ModelProps
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "The name is too long")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [MaxLength(200, ErrorMessage = "The email is too long")]
        [EmailAddress]
        public string ContactEmail { get; set; } 
        #endregion
        public string ErrorMsg { get; set; }

        public RelayCommand AddPublisherCommand { get; set; }

        public async void AddPublisher()
        {
            try
            {
                var publisher = new Publisher
                {
                    Name = Name,
                    ContactEmail = ContactEmail
                };
                var newPublisher = await publisherService.AddPublisherAsync(publisher);

                // sending the new publisher
                Messenger.Default.Send<Publisher>(newPublisher);
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

        private void CleanForm()
        {
            Name = "";
            ContactEmail = "";
            RaisePropertyChanged("Name");
            RaisePropertyChanged("ContactEmail");
        }
    }
}
