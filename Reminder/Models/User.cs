using FluentValidation;
using SDK.Base.ViewModels;

namespace Reminder.Models
{
    public class User : ViewModelBase
    {
        #region Private property
        private int _id;
        private string? _name;
        private string? _lastName;
        private string? _middleName;
        private string? _note;
        private string? _avatar;
        private DateTime _birthday = new DateTime(1989, 7, 29);
        #endregion

        #region Public property
        public int Id { get => _id; set => SetProperty(ref _id, value); }
        public string? Name { get => _name; set => SetProperty(ref _name, value); }
        public string? LastName { get => _lastName; set => SetProperty(ref _lastName, value); }
        public string? MiddleName { get => _middleName; set => SetProperty(ref _middleName, value); }
        public string? Note { get => _note; set => SetProperty(ref _note, value); }
        public string? Avatar { get => _avatar; set => SetProperty(ref _avatar, value); }
        public DateTime Birthday { get => _birthday; set => SetProperty(ref _birthday, value); }

        #endregion
    }
}
