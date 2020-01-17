using GalaSoft.MvvmLight.Views;

namespace ViewModels.Services
{
    public interface IFrameNavigationService : INavigationService
    {
        object Parameter { get; }
    }
}
