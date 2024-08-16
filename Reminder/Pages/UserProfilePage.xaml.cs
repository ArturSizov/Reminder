using Reminder.ViewModels;

namespace Reminder.Pages;

public partial class UserProfilePage
{
	public UserProfilePage(UserProfilePageViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}