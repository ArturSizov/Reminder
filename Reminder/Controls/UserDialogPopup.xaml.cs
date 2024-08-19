using CommunityToolkit.Maui.Views;
using Reminder.ViewModels.Popup;

namespace Reminder.Controls;

public partial class UserDialogPopup : Popup
{
    public UserDialogPopup(UserDialogPopupViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}

    private async void Yes_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(true);
    }

    private async void No_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(false);
    }
}