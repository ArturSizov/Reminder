using SDK.Base.Abstractions;
using SDK.Base.ViewModels;
using System.Windows.Input;

namespace Reminder.ViewModels
{
    public class AppShellViewModel : ViewModelBase
    {
        #region Private property
        /// <summary>
        ///  Flyout is presented
        /// </summary>
        private bool _flyoutIsPresented;

        /// <summary>
        /// Dialog service
        /// </summary>
        private readonly IDialogService _dialogService;

        #endregion

        #region Public property

        /// <summary>
        /// App title
        /// </summary>
        public string? Title { get; set; } = "Reminder";

        /// <summary>
        ///  Flyout is presented
        /// </summary>
        public bool FlyoutIsPresented { get => _flyoutIsPresented; set => SetProperty(ref _flyoutIsPresented, value); }

        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        public AppShellViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            OpenPageCommand = new Command<string>(OnOpenPageAsync);
        }


        #region Commands
        /// <summary>
        /// Open page command
        /// </summary>
        public ICommand OpenPageCommand { get; }

        private async void OnOpenPageAsync(string url)
        {
            await _dialogService.ShowLoadingAsync(SDK.Base.Properties.Resource.Loading);

            await Shell.Current.GoToAsync(url);

            FlyoutIsPresented = false;

            _dialogService.CloseLoadingPopup();
        }
        #endregion
    }
}
