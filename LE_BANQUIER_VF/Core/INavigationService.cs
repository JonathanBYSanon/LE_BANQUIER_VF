namespace LE_BANQUIER_VF.Core
{
    public interface INavigationService
    {   
        void NavigateTo(string viewName);
        void GoBack();
    }
}
