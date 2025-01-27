namespace LE_BANQUIER_VF.Core
{
    /// <summary>
    /// Interface for the navigation service
    /// </summary>
    public interface INavigationService
    {   
        void NavigateTo(string viewName);
        void GoBack();
    }
}
