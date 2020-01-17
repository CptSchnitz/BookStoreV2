namespace ViewModels.Services
{
    //Interface for showing popup messages for the user
    public interface IMessageBoxService
    {
        void ShowMessage(string title, string content);
        bool ShowYesNoMessage(string title, string content);
        bool ShowOkCancelMessage(string title, string content);
    }
}
