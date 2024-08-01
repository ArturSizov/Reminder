using Reminder.ViewModels;

namespace Reminder.Pages
{
    public partial class MainPage
    {
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
