namespace Reminder
{
    public partial class App
    {
        public App(AppShell shell)
        {
            InitializeComponent();

            MainPage = shell;
        }

        /// <summary>
        /// For debugging in Windows
        /// </summary>
        /// <param name="activationState"></param>
        /// <returns></returns>
        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 380;
            const int newHeight = 700;

            window.Width = newWidth;
            window.Height = newHeight;

            return window;
        }

    }
}
