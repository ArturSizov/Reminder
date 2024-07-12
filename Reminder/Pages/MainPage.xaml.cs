namespace Reminder.Pages
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SearchTextChanged(object sender, EventArgs e)
        {
            //string searchText = ((TextEdit)sender).Text;
            //dataGrid.FilterString = $"Contains([FirstName], '{searchText}') or Contains([LastName], '{searchText}')";
        }
    }
}
