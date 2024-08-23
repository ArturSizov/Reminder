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

        private void DataGridView_Scrolled(object sender, DevExpress.Maui.DataGrid.DataGridViewScrolledEventArgs e)
        {
            if(e.DeltaY > 0 )
            {
                buttonAdd.IsVisible = false;
                return;
            }
            buttonAdd.IsVisible = true;
        }
    }
}
