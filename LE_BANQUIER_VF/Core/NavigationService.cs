using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace LE_BANQUIER_VF.Core
{
    /// <summary>
    /// Service for navigation between views that take a content region as parameter and have a registered list of views
    /// </summary>
    public class NavigationService : INavigationService
    {
        private readonly ContentControl _contentRegion;
        private readonly Dictionary<string, Type> _views;

        public NavigationService(ContentControl contentRegion)
        {
            _contentRegion = contentRegion;
            _views = new Dictionary<string, Type>();
        }

        /// <summary>
        /// Register a view with a key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="viewType"></param>
        public void RegisterView(string key, Type viewType)
        {
            if (!_views.ContainsKey(key))
                _views.Add(key, viewType);
        }

        /// <summary>
        /// Navigate to a view by its key
        /// </summary>
        /// <param name="viewName"></param>
        /// <exception cref="ArgumentException"></exception>
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
