using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Services
{
    public interface IFrameNavigationService : INavigationService
    {
        object Parameter { get; }
    }
}
