using SDK.Base.Abstractions;
using SDK.Base.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #endregion

        public SettingsPageViewModel(IThemesManager themesManager)
        {
            ThemesManager = themesManager;
            SelectedTheme = themesManager.GetSelectedTheme();

            OpenPopupCommand = new Command(OnOpenPopupAsync);
            SetThemeCommand = new Command<AppTheme>(OnSetThemeAsync);
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
        #endregion
    }
}
