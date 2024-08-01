using DevExpress.Maui.Core;
using System.Windows.Input;

namespace Reminder.Controls;

public partial class MenuButtonControl
{
    #region ImageSource
    public string ImageSource { get => (string)GetValue(ImageSourceProperty); set { SetValue(ImageSourceProperty, value); } }

    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(MenuButtonControl), null);
    #endregion

    #region Text
    public string Text { get => (string)GetValue(TextProperty); set { SetValue(TextProperty, value); } }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MenuButtonControl), null);
    #endregion

    #region Command
    public ICommand Command { get => (Command)GetValue(CommandProperty); set { SetValue(CommandProperty, value); } }

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(MenuButtonControl), null);
    #endregion

    #region Command
    public object CommandParameter { get => (object)GetValue(CommandParameterProperty); set { SetValue(CommandParameterProperty, value); } }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MenuButtonControl), null);
    #endregion

    public MenuButtonControl()
	{
		InitializeComponent();
	}
}