using System.Windows;
using ViewModels.Services;

namespace WPF.Common.Services
{
    // service for displaying messages
    public class WpfMessageBoxService : IMessageBoxService
    {
        // message with just content
        public void ShowMessage(string title, string content)
        {
            MessageBox.Show(content, title);
        }

        // messages that shows ok and cancel buttons. if user presses ok method returns true.
        public bool ShowOkCancelMessage(string title, string content)
        {
            var result = MessageBox.Show(content, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK;
        }

        // messages that shows yes and no buttons. if user presses yes method returns true.

        public bool ShowYesNoMessage(string title, string content)
        {
            var result = MessageBox.Show(content, title, MessageBoxButton.YesNo);
            return result == MessageBoxResult.Yes;
        }
    }
}
