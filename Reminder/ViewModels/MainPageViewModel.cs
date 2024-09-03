﻿using System.Windows.Input;
using Reminder.Abstractions;
using Reminder.Models;
using Reminder.Pages;
using SDK.Base.Abstractions;
using SDK.Base.ViewModels;

namespace Reminder.ViewModels
{
    /// <summary>
    /// Main page VM
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        #region Private property
        /// <summary>
        /// Search by string
        /// </summary>
        private string? _searchText;

        /// <summary>
        /// Shows or hides the error
        /// </summary>
        private bool? _isVisebleErrorMessage = false;

        /// <summary>
        /// Dialog service
        /// </summary>
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Notification services
        /// </summary>
        private readonly INotificationServices _notificationServices;

        /// <summary>
        /// Disables the button while the command is running
        /// </summary>
        private bool? _addUserIsEnabled;

        #endregion

        #region Public property

        /// <summary>
        /// User data manager
        /// </summary>
        public IDataManager<User> DataManager { get; }

        /// <summary>
        /// Search by string
        /// </summary>
        public string? SearchText { get => _searchText; set => SetProperty(ref _searchText, value); }

        /// <summary>
        /// Shows or hides the error
        /// </summary>
        public bool? IsVisebleErrorMessage { get => _isVisebleErrorMessage; set => SetProperty(ref _isVisebleErrorMessage, value); }

        /// <summary>
        /// Disables the button while the command is running
        /// </summary>
        public bool? AddUserIsEnabled { get => _addUserIsEnabled; set => SetProperty(ref _addUserIsEnabled, value); }

        #endregion

        #region Ctor
        public MainPageViewModel(IThemesManager themes, IDataManager<User> dataManager, IDialogService dialogService, INotificationServices notificationServices)
        {
            DataManager = dataManager;

            themes.GetSelectedTheme();

            _dialogService = dialogService;
            _notificationServices = notificationServices;

            OpenUserProfileCommand = new Command<User>(OpenUserProfileAsync);
            SearchCommand = new Command<string>(Search);
            AddUserCommand = new Command(OnAddUserAsync);
            DeleteUserCommand = new Command<User>(OnDeleteUserAsync);
        }
        #endregion

        #region Commands
        /// <summary>
        /// Opens the user's profile
        /// </summary>
        public ICommand OpenUserProfileCommand { get; }

        /// <summary>
        /// Opens profile
        /// </summary>
        /// <param name="user"></param>
        private async void OpenUserProfileAsync(User user)
        {
            await _dialogService.ShowLoadingAsync(SDK.Base.Properties.Resource.Loading);

            await Shell.Current.GoToAsync(nameof(UserProfilePage), new Dictionary<string, object>
            {
                { nameof(UserProfilePage), new User
                    {
                        Name = user.Name,
                        Avatar = user.Avatar,
                        Birthday = user.Birthday,
                        LastName = user.LastName,
                        MiddleName = user.MiddleName,
                        Note = user.Note,
                        Id = user.Id
                    }
                }});

            _dialogService.CloseLoadingPopup();
        }

        /// <summary>
        /// Search by first name, last name, patronymic
        /// </summary>
        public ICommand SearchCommand { get; }

        /// <summary>
        /// Filters the list by entered text
        /// </summary>
        /// <param name="str"></param>
        private void Search(string str)
        {
            SearchText = $"Contains([Name], '{str}') or Contains([LastName], '{str}') or Contains([MiddleName], '{str}')";

            IsVisebleErrorMessage = !DataManager.Items.Any(u =>(u.Name != null && u.Name.Contains(str, StringComparison.CurrentCultureIgnoreCase)) 
                || u.LastName != null && u.LastName.Contains(str, StringComparison.CurrentCultureIgnoreCase)
                || u.MiddleName != null && u.MiddleName.Contains(str, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Add user command
        /// </summary>
        public ICommand AddUserCommand { get; }

        private async void OnAddUserAsync(object obj)
        {
           AddUserIsEnabled = await _dialogService.ShowLoadingAsync(SDK.Base.Properties.Resource.Loading);

           await Shell.Current.GoToAsync(nameof(UserProfilePage));

           AddUserIsEnabled = _dialogService.CloseLoadingPopup();
        }

        /// <summary>
        /// Delete user command
        /// </summary>
        public ICommand DeleteUserCommand { get; }

        private async void OnDeleteUserAsync(User user)
        {
            var result = await _dialogService.ShowPopupAsync($"{SDK.Base.Properties.Resource.DeleteUser} {user.Name}?");

            if (result == true && user.Id != null)
            {
                await DataManager.DeleteAsync(user);
                _notificationServices.Cancel((int)user.Id);
            }
        }
        #endregion

        #region Methods
   
        #endregion
    }
}
