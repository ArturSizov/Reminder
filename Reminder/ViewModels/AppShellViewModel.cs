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
        public AppShellViewModel()
        {
            OpenPageCommand = new Command<string>(OnOpenPageAsync);
        }

        #region Commands
        /// <summary>
        /// Open page command
        /// </summary>
        public ICommand OpenPageCommand { get; }

        private async void OnOpenPageAsync(string url)
        {
            await Shell.Current.GoToAsync(url);

            FlyoutIsPresented = false;
        }
        #endregion
    }
}
