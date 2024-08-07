using Reminder.ViewModels;

namespace Reminder.Pages;

public partial class SettingsPage
{
	public SettingsPage(SettingsPageViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}