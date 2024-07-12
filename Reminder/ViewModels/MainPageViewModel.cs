using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Reminder.Models;
using SDK.ViewModels;
using SDK.ViewModels.ViewModels;

namespace Reminder.ViewModels
{
    /// <summary>
    /// Main page VM
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        #region Private property
        private string _title = "Reminder";
        #endregion

        #region Public property
        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public ObservableCollection<User> Users { get; set; } = new();
        #endregion

        #region Ctor
        public MainPageViewModel()
        {
            OpenUserProfileCommand = new Command(OpenUserProfile);

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
                Birthday = DateTime.Now,
                MiddleName = "Фликсовна",
                Avatar = null
            });
            Users.Add(new User
            {
                Id = 3,
                Name = "Эльмира",
                Position = "Жена",
                Birthday = DateTime.Now,
                MiddleName = "Фликсовна",
                Avatar = null
            });
            Users.Add(new User
            {
                Id = 3,
                Name = "Эльмира",
                Position = "Жена",
                Birthday = DateTime.Now,
                MiddleName = "Фликсовна",
                Avatar = null
            });
            Users.Add(new User
            {
                Id = 3,
                Name = "Эльмира",
                Position = "Жена",
                Birthday = DateTime.Now,
                MiddleName = "Фликсовна",
                Avatar = null
            });
            Users.Add(new User
            {
                Id = 3,
                Name = "Эльмира",
                Position = "Жена",
                Birthday = DateTime.Now,
                MiddleName = "Фликсовна",
                Avatar = null
            });
            Users.Add(new User
            {
                Id = 3,
                Name = "Эльмира",
                Position = "Жена",
                Birthday = DateTime.Now,
                MiddleName = "Фликсовна",
                Avatar = null
            });
        }
     
        #endregion

        #region Commands
        /// <summary>
        /// Opens the user's profile
        /// </summary>
        public ICommand OpenUserProfileCommand { get; }

        private void OpenUserProfile(object obj)
        {
            
        }
        #endregion
    }
}
