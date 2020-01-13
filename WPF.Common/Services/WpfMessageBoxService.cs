using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using ViewModels.Services;

namespace WPF.Common.Services
{
    public class WpfMessageBoxService : IMessageBoxService
    {
        public void ShowMessage(string title, string content)
        {
            MessageBox.Show(content, title);
        }

        public bool ShowOkCancelMessage(string title, string content)
        {
            var result = MessageBox.Show(content, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK;
        }

        public bool ShowYesNoMessage(string title, string content)
        {
            var result = MessageBox.Show(content, title, MessageBoxButton.YesNo);
            return result == MessageBoxResult.Yes;
        }
    }
}
