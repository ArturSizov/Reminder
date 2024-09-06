﻿using Reminder.Abstractions;
using Reminder.Models;
using Reminder.Pages;
using SDK.Base.Abstractions;
using SDK.Base.Extensions;
using SDK.Base.ViewModels;
using System.Runtime;
using System.Windows.Input;

namespace Reminder.ViewModels
{
    /// <summary>
    /// User profile page view model 
    /// </summary>
    [QueryProperty(nameof(User), nameof(UserProfilePage))]
    public class UserProfilePageViewModel : ViewModelBase
    {
        #region Private property
        /// <summary>
        /// Is open popup
        /// </summary>
        private bool _isOpenPopup;

        /// <summary>
        /// User model
        /// </summary>
        private User? _user = new();

        /// <summary>
        /// User data manager
        /// </summary>
        private readonly IDataManager<User> _dataManager;

        /// <summary>
        /// Photo manager
        /// </summary>
        private readonly IPhotoManager _photoManager;

        /// <summary>
        /// Dialog service
        /// </summary>
        private readonly IDialogService _dialog;

        /// <summary>
        /// Notification services
        /// </summary>
        private readonly INotificationServices _notificationServices;

        /// <summary>
        /// App settings
        /// </summary>
        private readonly IAppSettings _settings;

        #endregion

        #region Public property
        /// <summary>
        /// Is open popup
        /// </summary>
        public bool IsOpenPopup { get => _isOpenPopup; set => SetProperty(ref _isOpenPopup, value); }

        /// <summary>
        /// User model
        /// </summary>
        public User? User { get => _user; set => SetProperty(ref _user, value); }

        #endregion

        public UserProfilePageViewModel(IPhotoManager photoManager, IDataManager<User> dataManager, IDialogService dialog, INotificationServices notificationServices,
            IAppSettings settings)
        {
            _dataManager = dataManager;
            _photoManager = photoManager;
            _dialog = dialog;
            _notificationServices = notificationServices;
            _settings = settings;

            OpenPopupCommand = new Command(OnOpenPopupAsync);
            TakePhotoCommand = new Command(OnTakePhotoAsync);
            OpenImageCommand = new Command(OnOpenImageAsync);
            DeleteImageCommand = new Command(OnDeleteImageAsync);
            SaveUserCommand = new Command(OnSaveUserAsync);
        }

        #region Commands
        /// <summary>
        /// Open popup command
        /// </summary>
        public ICommand OpenPopupCommand { get; }

        private void OnOpenPopupAsync(object obj) => IsOpenPopup = !IsOpenPopup;

        /// <summary>
        /// Take a photo using the camera
        /// </summary>
        public ICommand TakePhotoCommand { get; }
       
        private async void OnTakePhotoAsync(object obj)
        {
            IsOpenPopup = false;

            if (User != null)
              User.Avatar = await _photoManager.TakePhotoAsync();
        }

        /// <summary>
        /// Open photo from gallery
        /// </summary>
        public ICommand OpenImageCommand { get; }

        private async void OnOpenImageAsync(object obj)
        {
            IsOpenPopup = false;

            if (User != null)
                User.Avatar = await _photoManager.AddPhotoGalleryAsync();
        }

        /// <summary>
        /// Delete image command
        /// </summary>
        public ICommand DeleteImageCommand { get; }

        private async void OnDeleteImageAsync(object obj)
        {
            IsOpenPopup = false;

            var result = await _dialog.ShowPopupAsync(SDK.Base.Properties.Resource.DeleteAvatar);

            if (User != null && result == true)
                User.Avatar = _photoManager.Delete(User?.Avatar);
        }

        /// <summary>
        /// Save user command
        /// </summary>
        public ICommand SaveUserCommand { get; }

        private async void OnSaveUserAsync(object obj)
        {
            if (User == null)
                return;

            if(User.Id == 0)
            {
                if(!await _dataManager.CreateAsync(User))
                {
                    //TODO: error message
                    return;
                }
            }   
            else
            {
                if (!await _dataManager.UpdateAsync(User))
                {
                    //TODO: error message
                    return;
                }
                _notificationServices.Cancel(User.Id);
            }              

            await _notificationServices.AddNotificationAsync(User.Id, $"{SDK.Base.Properties.Resource.Title}: {User.LastName} {User.Name} {User.MiddleName}.", SDK.Base.Properties.Resource.Congratulate, User.Birthday, _settings.Time);

            await Shell.Current.Navigation.PopAsync();
        }
        #endregion
    }
}
