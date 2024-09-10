using SDK.Base.Abstractions;

namespace Reminder.Services
{
    /// <summary>
    /// Navigation service 
    /// </summary>
    public class CustomNavigationService : ICustomNavigationService
    {
        /// <inheritdoc/>
        public Task NavigateToAsync(string route, IDictionary<string, object> ?routeParameters = null) => routeParameters != null
                    ? Shell.Current.GoToAsync(route, routeParameters)
                    : Shell.Current.GoToAsync(route);
    }
}
