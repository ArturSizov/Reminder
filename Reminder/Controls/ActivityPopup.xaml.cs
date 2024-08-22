using CommunityToolkit.Maui.Views;
using SDK.Base.ViewModels.Popup;

namespace Reminder.Controls;

public partial class ActivityPopup : Popup
{
	public ActivityPopup(ActivityPopupViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}