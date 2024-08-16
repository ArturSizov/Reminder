namespace Reminder.Controls;

public partial class UserAvatar
{
    #region ImageSource
    public object ImageSource { get => (object)GetValue(ImageSourceProperty); set { SetValue(ImageSourceProperty, value); } }

    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(object), typeof(UserAvatar));

    #endregion

    public UserAvatar()
	{
		InitializeComponent();
    }
}