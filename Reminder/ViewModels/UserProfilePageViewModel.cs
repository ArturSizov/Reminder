using CommunityToolkit.Maui.Core;
using Reminder.Controls;
using Reminder.Models;
using Reminder.Pages;
using Reminder.ViewModels.Popup;
using SDK.Base.Abstractions;
using SDK.Base.Properties;
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
        private User? _user;

        /// <summary>
        /// Photo manager
        /// </summary>
        private readonly IPhotoManager _photoManager;

        /// <summary>
        /// Popup service
        /// </summary>
        private readonly IPopupService _popup;

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

        public UserProfilePageViewModel(IPhotoManager photoManager, IPopupService popup)
        {
            _photoManager = photoManager;
            _popup = popup;

            OpenPopupCommand = new Command(OnOpenPopupAsync);
            TakePhotoCommand = new Command(OnTakePhotoAsync);
            OpenImageCommand = new Command(OnOpenImageAsync);
            DeleteImageCommand = new Command(OnDeleteImage);
        }

        #region Commands
        /// <summary>
        /// Open popup command
        /// </summary>
        public ICommand OpenPopupCommand { get; }

        private void OnOpenPopupAsync(object obj) => IsOpenPopup = !IsOpenPopup;
        #endregion

        #region TakePhotoCommand

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

        private async void OnDeleteImage(object obj)
        {
            IsOpenPopup = false;

            var result = (bool?)await _popup.ShowPopupAsync<UserDialogPopupViewModel>(onPresenting: viewModel => 
                                                                                     viewModel.Text=Resource.DeleteAvatar);

            if (User != null && result == true)
                User.Avatar = null;
        }
        #endregion
    }
}
