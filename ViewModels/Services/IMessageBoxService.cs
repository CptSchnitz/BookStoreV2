using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Services
{
    public interface IMessageBoxService
    {
        void ShowMessage(string title, string content);
        bool ShowYesNoMessage(string title, string content);
        bool ShowOkCancelMessage(string title, string content);
    }
}
