using Reminder.Abstractions;
using Reminder.Models;
using SDK.Base.Abstractions;
using SDK.Base.Extensions;
using SDK.Base.ViewModels;
using System.Windows.Input;

namespace Reminder.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        #region Private property
        /// <summary>
        /// Is open popup
        /// </summary>
        private bool _isOpenPopup;

        /// <summary>
        /// Select user app thema
        /// </summary>
        private string? _selectedTheme;

        /// <summary>
        /// Reminders enabled/disabled
        /// </summary>
        private bool _isChecked;

        /// <summary>
        /// Alert time
        /// </summary>
        private DateTime _time;

        /// <summary>
        /// Notification service
        /// </summary>
        private readonly IUserNotificationServices _notificationServices;

        /// <summary>
        /// App settings
        /// </summary>
        private readonly IAppSettings _settings;

        /// <summary>
        /// Data manager
        /// </summary>
        private readonly IDataManager<User> _dataManager;

        /// <summary>
        /// Dialog service
        /// </summary>
        private readonly IDialogService _dialog;

        #endregion

        #region Public property
        /// <summary>
        /// Is open popup
        /// </summary>
        public bool IsOpenPopup { get => _isOpenPopup; set => SetProperty(ref _isOpenPopup, value); }

        /// <summary>
        /// Select user app thema
        /// </summary>
        public string? SelectedTheme { get => _selectedTheme; set => SetProperty(ref _selectedTheme, value); }

        /// <summary>
        /// Reminders enabled/disabled
        /// </summary>
        public bool IsChecked { get => _isChecked; set => SetProperty(ref _isChecked, value); }

        /// <summary>
        /// Themes manager
        /// </summary>
        public IThemesManager ThemesManager { get; }

        /// <summary>
        /// Alert time
        /// </summary>
        public DateTime Time { get => _time; set => SetProperty(ref _time, value); }

        #endregion

        public SettingsPageViewModel(IThemesManager themesManager, IUserNotificationServices notificationServices, IAppSettings settings, IDataManager<User> dataManager,
            IDialogService dialog)
        {
            ThemesManager = themesManager;
            _notificationServices = notificationServices;
            _settings = settings;
            _dataManager = dataManager;
            _dialog = dialog;

            SelectedTheme = themesManager.GetSelectedTheme();
            Time = _settings.Time;
            IsChecked = notificationServices.IsEnabled;

            OpenPopupCommand = new Command(OnOpenPopupAsync);
            SetThemeCommand = new Command<AppTheme>(OnSetThemeAsync);
            SetTimeCommand = new Command(OnSetTime);
            CheckedCommand = new Command(OnChecked);
        }

        #region Commands

        /// <summary>
        /// Open popup command
        /// </summary>
        public ICommand OpenPopupCommand{ get; }

        private void OnOpenPopupAsync(object obj) => IsOpenPopup = !IsOpenPopup;

        /// <summary>
        /// Set app theme command
        /// </summary>
        public ICommand SetThemeCommand { get; }

        private void OnSetThemeAsync(AppTheme theme)
        {
            IsOpenPopup = false;
            SelectedTheme = ThemesManager.SetTheme(theme);
        }

        /// <summary>
        /// Sets the alert time
        /// </summary>
        public ICommand SetTimeCommand { get; }

        private async void OnSetTime(object obj)
        {
            _notificationServices.CancelAll();
            _settings.Time = Time;

            await SetNotificationUsersAsync();
        }

        /// <summary>
        /// Checked command
        /// </summary>
        public ICommand CheckedCommand { get; }

        private async void OnChecked(object obj)
        {
            _notificationServices.IsEnabled = IsChecked;

            if (!IsChecked)
            {
                _notificationServices.CancelAll();
                _dialog.ShowInformationMessage(SDK.Base.Properties.Resource.NotificationsDisabled);
                return;
            }

            await SetNotificationUsersAsync();

            _dialog.ShowInformationMessage(SDK.Base.Properties.Resource.NotificationsEnabled);
        }

        /// <summary>
        /// Sets reminders for all users
        /// </summary>
        private async Task SetNotificationUsersAsync()
        {
            foreach (var item in _dataManager.Items)
                await _notificationServices.AddNotificationAsync(item.Id, $"{SDK.Base.Properties.Resource.Title}: {item.LastName} {item.Name} {item.MiddleName}", SDK.Base.Properties.Resource.Congratulate, item.Birthday, Time);
        }
        #endregion
    }
}
