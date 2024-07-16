namespace Reminder.Controls;

public partial class UserAvatar
{
    #region ImageSource
    public string ImageSource { get => (string)GetValue(ImageSourceProperty); set { SetValue(ImageSourceProperty, value); } }

    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(UserAvatar));

    #endregion

    public UserAvatar()
	{
		InitializeComponent();
    }
}