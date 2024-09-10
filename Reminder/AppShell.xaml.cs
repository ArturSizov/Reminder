using Reminder.Abstractions;
using Reminder.Models;
using Reminder.Pages;
using Reminder.ViewModels;
using SDK.Base.Abstractions;

namespace Reminder
{
    public partial class AppShell
    {
        #region Private property

        /// <summary>
        /// Data manager
        /// </summary>
        private readonly IDataManager<User> _dataManager;

        /// <summary>
        /// Navigation service
        /// </summary>
        private readonly ICustomNavigationService _navigationService;

        /// <summary>
        /// User model
        /// </summary>
        private User? _user;

        #endregion

        public AppShell(AppShellViewModel vm, IUserNotificationServices notificationServices, IDataManager<User> dataManager, ICustomNavigationService navigationService)
        {
            _dataManager = dataManager;
            _navigationService = navigationService;

            InitializeComponent();

            RegisterRoute();

            BindingContext = vm;

            notificationServices.NotificationService.NotificationActionTapped += NotificationActionTapped;
        }

        #region Methods

        /// <summary>
        /// Fires when push notifications are clicked
        /// </summary>
        /// <param name="e"></param>
        private async void NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
        {
            if(e.IsTapped)
            {
                await _dataManager.ReadAllUsersAsync();
                _user = await _dataManager.ReadAsync(e.Request.NotificationId);

                OpenUserProfileAsync();
            }
        }

        protected async override void OnParentSet()
        {
            base.OnParentSet();

            await _dataManager.ReadAllUsersAsync();
        }


        /// <summary>
        /// Pages register
        /// </summary>
        private void RegisterRoute()
        {
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));

            Routing.RegisterRoute(nameof(UserProfilePage), typeof(UserProfilePage));

            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }

        /// <summary>
        /// Open tapped user
        /// </summary>
        private async void OpenUserProfileAsync()
        {
            if (_user == null)
                return;

            await _navigationService.NavigateToAsync(nameof(UserProfilePage), new Dictionary<string, object>
                {
                    { nameof(UserProfilePage), new User
                        {
                            Name = _user.Name,
                            Avatar = _user.Avatar,
                            Birthday = _user.Birthday,
                            LastName = _user.LastName,
                            MiddleName = _user.MiddleName,
                            Note = _user.Note,
                            Id = _user.Id
                        }
                }});
        }

        #endregion
    }
}
