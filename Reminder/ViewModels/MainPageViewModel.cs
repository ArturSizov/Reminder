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
                Avatar = "https://www.eg.ru/wp-content/uploads/2021/03/coy105122.jpg"
            });
            Users.Add(new User
            {
                Id = 1,
                Name = "Амир",
                Position = "Боксер",
                LastName = "Сизов",
                Birthday = DateTime.Now,
                MiddleName = "Артурович",
                Avatar = "https://live.staticflickr.com/65535/52211883534_f45cb76810_z.jpg"
            });
            Users.Add(new User
            {
                Id = 2,
                Name = "Адель",
                Position = "Шеф",
                LastName = "Сизов",
                Birthday = DateTime.Now,
                MiddleName = "Артурович",
                Avatar = "https://images.thevoicemag.ru/upload/img_cache/272/2725c992c4d156f467638a9590d68900_cropped_666x442.jpg"
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
                MiddleName = "Георгиевич",
                Avatar = "https://vdnh.ru/upload/resize_cache/iblock/601/1000_424_1/6010c3df7633397b251fa9ccddf89830.jpg"
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
