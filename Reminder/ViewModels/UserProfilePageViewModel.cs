using Reminder.Abstractions;
using Reminder.Models;
using Reminder.Pages;
using Reminder.Validators;
using SDK.Base.Abstractions;
using SDK.Base.Extensions;
using SDK.Base.ViewModels;
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
        /// Disables the button while the command is running
        /// </summary>
        private bool? _saveUserIsEnabled;

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
        private readonly IUserNotificationServices _notificationServices;

        /// <summary>
        /// App settings
        /// </summary>
        private readonly IAppSettings _settings;

        /// <summary>
        /// Navigation service
        /// </summary>
        private readonly ICustomNavigationService _navigationService;

        /// <summary>
        /// Validator
        /// </summary>
        private UserValidator _validator = new();

        /// <summary>
        /// Has error validate
        /// </summary>
        private bool? _hasError;

        /// <summary>
        /// Validation error text
        /// </summary>
        private string? _errorText;

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

        /// <summary>
        /// Disables the button while the command is running
        /// </summary>
        public bool? SaveUserIsEnabled { get => _saveUserIsEnabled; set => SetProperty(ref _saveUserIsEnabled, value); }

        /// <summary>
        /// Has error validate
        /// </summary>
        public bool? HasError { get => _hasError; set => SetProperty(ref _hasError, value); }


        /// <summary>
        /// Validation error text
        /// </summary>
        public string? ErrorText { get => _errorText; set => SetProperty(ref _errorText, value); }

        #endregion

        public UserProfilePageViewModel(IPhotoManager photoManager, IDataManager<User> dataManager, IDialogService dialog, IUserNotificationServices notificationServices,
            IAppSettings settings, ICustomNavigationService navigationService)
        {
            _dataManager = dataManager;
            _photoManager = photoManager;
            _dialog = dialog;
            _notificationServices = notificationServices;
            _settings = settings;
            _navigationService = navigationService;

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
            {
                var result = await _photoManager.TakePhotoAsync();

                if(result == null)
                {
                    _dialog.ShowErrorMessage(SDK.Base.Properties.Resource.ErrorAddingPhoto);
                    return;
                }

                User.Avatar = result;
            }            
        }

        /// <summary>
        /// Open photo from gallery
        /// </summary>
        public ICommand OpenImageCommand { get; }

        private async void OnOpenImageAsync(object obj)
        {
            IsOpenPopup = false;

            if (User != null)
            {
                var result = await _photoManager.AddPhotoGalleryAsync();

                if(result == null)
                {
                    _dialog.ShowErrorMessage(SDK.Base.Properties.Resource.ErrorAddingPhoto);
                    return;
                }

                User.Avatar = result;
            }   
        }

        /// <summary>
        /// Delete image command
        /// </summary>
        public ICommand DeleteImageCommand { get; }

        private async void OnDeleteImageAsync(object obj)
        {
            IsOpenPopup = false;

            var result = await _dialog.ShowPopupAsync(SDK.Base.Properties.Resource.DeleteAvatar);

            if (result == true)
            {
                if (!_photoManager.Delete(User?.Avatar))
                {
                    _dialog.ShowErrorMessage(SDK.Base.Properties.Resource.ErrorDeletingPhoto);
                    return;
                }

                if(User != null)
                   User.Avatar = null;
            }         
        }

        /// <summary>
        /// Save user command
        /// </summary>
        public ICommand SaveUserCommand { get; }

        private async void OnSaveUserAsync(object obj)
        {
            if (User == null)
                return;

            HasError = !await ValidateAsync();

            if (HasError == true)
                return;

            if (User.Id == 0)
            {
                if (!await _dataManager.CreateAsync(User))
                {
                    _dialog.ShowErrorMessage(SDK.Base.Properties.Resource.FailedCreate);
                    return;
                }
            }
            else
            {
                if (!await _dataManager.UpdateAsync(User))
                {
                    _dialog.ShowErrorMessage(SDK.Base.Properties.Resource.FailedUpdate);
                    return;
                }
                _notificationServices.Cancel(User.Id);
            }

            await _notificationServices.AddNotificationAsync(User.Id, $"{SDK.Base.Properties.Resource.Title}: {User.LastName} {User.Name} {User.MiddleName}", SDK.Base.Properties.Resource.Congratulate, User.Birthday, _settings.Time);

            SaveUserIsEnabled = await _dialog.ShowLoadingAsync(SDK.Base.Properties.Resource.Saving);

            await _navigationService.PopAsync();

            SaveUserIsEnabled = _dialog.CloseLoadingPopup();
        }

        /// <summary>
        /// User validate
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ValidateAsync()
        {
            if (User == null)
                return false;

            var result = await _validator.ValidateAsync(User);


            if (!result.IsValid)
                ErrorText = result.Errors[0].ErrorMessage;

            return result.IsValid;
        }
        #endregion
    }
}
