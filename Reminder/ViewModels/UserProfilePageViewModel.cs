using Reminder.Models;
using Reminder.Pages;
using SDK.Base.ViewModels;

namespace Reminder.ViewModels
{
    /// <summary>
    /// User profile page view model 
    /// </summary>
    [QueryProperty(nameof(User), nameof(UserProfilePage))]
    public class UserProfilePageViewModel : ViewModelBase
    {
        private string? _title = "User profile";

        public string? Title { get => _title; set => SetProperty(ref _title, value); }

        private User? _user;

        public User? User { get => _user; set => SetProperty(ref _user, value); }
    }
}
