using Reminder.Abstractions;
using Reminder.Models;
using SDK.Base.Abstractions;
using SDK.Base.Extensions;
using SDK.Base.ViewModels;
using System.Runtime;
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
        /// Alert time
        /// </summary>
        private DateTime _time;

        /// <summary>
        /// Notification service
        /// </summary>
        private readonly INotificationServices _notificationServices;

        /// <summary>
        /// App settings
        /// </summary>
        private readonly IAppSettings _settings;

        /// <summary>
        /// Data manager
        /// </summary>
        private readonly IDataManager<User> _dataManager;

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
        /// Themes manager
        /// </summary>
        public IThemesManager ThemesManager { get; }

        /// <summary>
        /// Alert time
        /// </summary>
        public DateTime Time { get => _time; set => SetProperty(ref _time, value); }

        #endregion

        public SettingsPageViewModel(IThemesManager themesManager, INotificationServices notificationServices, IAppSettings settings, IDataManager<User> dataManager)
        {
            ThemesManager = themesManager;
            _notificationServices = notificationServices;
            _settings = settings;
            _dataManager = dataManager;

            SelectedTheme = themesManager.GetSelectedTheme();
            Time = _settings.Time;

            OpenPopupCommand = new Command(OnOpenPopupAsync);
            SetThemeCommand = new Command<AppTheme>(OnSetThemeAsync);
            SetTimeCommand = new Command(OnSetTime);
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

        private void OnSetTime(object obj)
        {
            _notificationServices.CancelAll();
            _settings.Time = Time;

            foreach (var item in _dataManager.Items)
            {
                if (item.Id == null)
                    return;

                _notificationServices.AddNotificationAsync((int)item.Id, $"{SDK.Base.Properties.Resource.Title}: {item.LastName} {item.Name} {item.MiddleName}.", SDK.Base.Properties.Resource.Congratulate, item.Birthday, _settings.Time);
            }
        }
        #endregion
    }
}
