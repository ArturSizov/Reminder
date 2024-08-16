using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.Core;
using Microsoft.Maui;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Reminder.Managers;
using Reminder.Models;
using Reminder.Pages;
using SDK.Base.Abstractions;
using SDK.Base.Themes;
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

        #endregion

        #region Public property

        /// <summary>
        /// Search by string
        /// </summary>
        public string? SearchText { get => _searchText; set => SetProperty(ref _searchText, value); }

        /// <summary>
        /// Shows or hides the error
        /// </summary>
        public bool? IsVisebleErrorMessage { get => _isVisebleErrorMessage; set => SetProperty(ref _isVisebleErrorMessage, value); }

        public ObservableCollection<User> Users { get; set; } = new();
        #endregion

        #region Ctor
        public MainPageViewModel(IThemesManager themes)
        {
            themes.GetSelectedTheme();

            OpenUserProfileCommand = new Command<User>(OpenUserProfileAsync);

            SearchCommand = new Command<string>(Search);

            AddUserCommand = new Command(OnAddUserAsync);

            Users.Add(new User
            {
                Id = 0,
                Name = "Артур",
                Position = "Руководитель",
                LastName = "Сизов",
                Birthday = DateTime.Now,
                MiddleName = "Геннадьевич",
                Avatar = "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"
            });
            Users.Add(new User
            {
                Id = 1,
                Name = "Амир",
                Position = "Боксер",
                LastName = "Сизов",
                Birthday = DateTime.Now,
                MiddleName = "Артурович"
            });
            Users.Add(new User
            {
                Id = 2,
                Name = "Адель",
                Position = "Шеф",
                LastName = "Сизов",
                Birthday = DateTime.Now,
                MiddleName = "Артурович"
            });
            Users.Add(new User
            {
                Id = 3,
                Name = "Эльмира",
                Position = "Жена",
                Avatar = null,
                Birthday = DateTime.Now,
                MiddleName = "Фликсовна"
            });
            Users.Add(new User
            {
                Id = 3,
                Name = "Эльмира",
                Position = "Жена",
                Avatar = null,
                Birthday = DateTime.Now,
                MiddleName = "Фликсовна"
            });
            Users.Add(new User
            {
                Id = 3,
                Name = "Эльмира",
                Position = "Жена",
                Avatar = null,
                Birthday = DateTime.Now,
                MiddleName = "Фликсовна"
            });
            Users.Add(new User
            {
                Id = 3,
                Name = "Эльмира",
                Position = "Жена",
                Avatar = null,
                Birthday = DateTime.Now,
                MiddleName = "Фликсовна"
            });
            Users.Add(new User
            {
                Id = 3,
                Name = "Эльмира",
                Position = "Жена",
                Avatar = null,
                Birthday = DateTime.Now,
                MiddleName = "Фликсовна"
            });
            Users.Add(new User
            {
                Id = 3,
                Name = "Эльмира",
                Position = "Жена",
                Avatar = null,
                Birthday = DateTime.Now,
                MiddleName = "Фликсовна"
            });
            Users.Add(new User
            {
                Id = 3,
                Name = "Эльмира",
                Position = "Жена",
                Avatar = null,
                Birthday = DateTime.Now,
                MiddleName = "Фликсовна"
            });
            Users.Add(new User
            {
                Id = 4,
                Name = "Ленар",
                LastName = "Шабаев",
                Birthday = DateTime.Now,
                MiddleName = "Георгиевич"
            });

            if (Users.Count == 0)
                IsVisebleErrorMessage = true;
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
            await Shell.Current.GoToAsync(nameof(UserProfilePage), new Dictionary<string, object>
            {
                { nameof(UserProfilePage), new User
                    {
                        Name = user.Name,
                        Avatar = user.Avatar,
                        Birthday = user.Birthday,
                        LastName = user.LastName,
                        MiddleName = user.MiddleName,
                        Position = user.Position,
                        Id = user.Id
                    }
                }});
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

            IsVisebleErrorMessage = !Users.Any(u =>(u.Name != null && u.Name.Contains(str, StringComparison.CurrentCultureIgnoreCase)) 
                || u.LastName != null && u.LastName.Contains(str, StringComparison.CurrentCultureIgnoreCase)
                || u.MiddleName != null && u.MiddleName.Contains(str, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Add user command
        /// </summary>
        public ICommand AddUserCommand { get; }

        private void OnAddUserAsync(object obj)
        {

        }
        #endregion
    }
}
