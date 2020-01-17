using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ViewModels.Services;

namespace WPF.Common.Services
{
    // Service that handles navigation in wpf app.
    // Must have a frame called MainFrame in the app.
    public class FrameNavigationService : IFrameNavigationService, INotifyPropertyChanged
    {
        // dictionary of all the pages by key.
        private readonly Dictionary<string, Uri> _pagesByKey;

        private readonly List<string> _historic;
        private string _currentPageKey;

        // The page the frame is currently on
        public string CurrentPageKey
        {
            get
            {
                return _currentPageKey;
            }

            private set
            {
                if (_currentPageKey == value)
                {
                    return;
                }

                _currentPageKey = value;
                OnPropertyChanged("CurrentPageKey");
            }
        }

        // the navigation parameter
        public object Parameter { get; private set; }

        public FrameNavigationService()
        {
            _pagesByKey = new Dictionary<string, Uri>();
            _historic = new List<string>();
        }

        // goes to the previous page if there is one
        public void GoBack()
        {
            if (_historic.Count > 1)
            {
                _historic.RemoveAt(_historic.Count - 1);
                NavigateTo(_historic.Last(), null);
            }
        }
        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public virtual void NavigateTo(string pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                // checks if the page exists in the service.
                if (!_pagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(string.Format("No such page: {0} ", pageKey), "pageKey");
                }

                // Get the frame from the app main window.
                var frame = GetDescendantFromName(Application.Current.MainWindow, "MainFrame") as Frame;

                if (frame != null)
                {
                    // the actual navigation.
                    frame.Source = _pagesByKey[pageKey];
                }
                Parameter = parameter;

                // adds the new page to the history and sets the current page.
                _historic.Add(pageKey);
                CurrentPageKey = pageKey;
            }
        }

        // method for adding new pages to the service.
        public void Configure(string key, Uri pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(key))
                {
                    _pagesByKey[key] = pageType;
                }
                else
                {
                    _pagesByKey.Add(key, pageType);
                }
            }
        }

        // a recursive fucntion to find a specific element in the element tree.
        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            // loops over all the element childern
            for (var i = 0; i < count; i++)
            {
                var frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (frameworkElement != null)
                {
                    // correct element found
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    // checks if the element exists in the descendants
                    frameworkElement = GetDescendantFromName(frameworkElement, name);

                    //checks the element was found in the descendants.
                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }
            // element not found
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
