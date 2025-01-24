using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace LE_BANQUIER_VF.Core
{
    public class NavigationService : INavigationService
    {
        private readonly ContentControl _contentRegion;
        private readonly Dictionary<string, Type> _views;

        public NavigationService(ContentControl contentRegion)
        {
            _contentRegion = contentRegion;
            _views = new Dictionary<string, Type>();
        }

        public void RegisterView(string key, Type viewType)
        {
            if (!_views.ContainsKey(key))
                _views.Add(key, viewType);
        }

        public void NavigateTo(string viewName)
        {
            if (_views.ContainsKey(viewName))
            {
                var view = Activator.CreateInstance(_views[viewName]) as UserControl;
                _contentRegion.Content = view;
            }
            else
            {
                throw new ArgumentException($"View '{viewName}' not registered.");
            }
        }

        public void GoBack()
        {
            // Implement a navigation stack if needed
            throw new NotImplementedException();
        }
    }
}
