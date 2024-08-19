using SDK.Base.ViewModels;

namespace Reminder.ViewModels.Popup
{
    public class UserDialogPopupViewModel : ViewModelBase
    {
        #region Private property
        private string? _text;

        #endregion

        #region Public property
        public string? Text { get => _text; set => SetProperty(ref _text, value); }
        #endregion
    }
}
