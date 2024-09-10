using Reminder.ViewModels;
using SDK.Base.Auxiliary;

namespace Reminder.IoCModules
{
    /// <summary>
    /// VM locator
    /// </summary>
    public class ViewModelLocator
    {
        public MainPageViewModel MainPageViewModel => RootContainer.Container.Resolve<MainPageViewModel>();
        public UserProfilePageViewModel UserProfilePageViewModel => RootContainer.Container.Resolve<UserProfilePageViewModel>();
        public SettingsPageViewModel SettingsPageViewModel => RootContainer.Container.Resolve<SettingsPageViewModel>();
    }
}
