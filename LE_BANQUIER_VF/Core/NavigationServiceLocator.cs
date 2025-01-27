namespace LE_BANQUIER_VF.Core
{
    /// <summary>
    /// Static class to hold the navigation service so it can be accessed from anywhere
    /// </summary>
    public static class NavigationServiceLocator
    {
        public static NavigationService NavigationService { get; set; }
    }
}
